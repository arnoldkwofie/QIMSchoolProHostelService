using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Qface.Application.Shared.Common.Interfaces;
using Qface.Domain.Shared.Enums;
using QIMSchoolPro.Hostel.Domain.Constants;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Migrations;
using QIMSchoolPro.Hostel.Persistence.Repositories;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Exceptions;
using QIMSchoolPro.Hostel.Processors.Helpers;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QIMSchoolPro.Hostel.Processors.Processors
{
    [ProcessorBase]
    public class BookingProcessor
    {
      
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly IAcademicConfigurationRepository _acdemicConfigurationRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBedRepository _bedRepository;
        private readonly string _connectionString;
        public readonly ILogger<BookingProcessor> _logger;
        private readonly IDatabase _database;
        private readonly IRoomRepository _roomRepository;
        private readonly IConnectionMultiplexer _redis;
        private readonly RoomProcessor _roomProcessor;
        private readonly IHubContext<BookingHub> _hubContext;
        private readonly StudentProcessor _studentProcessor;



        public BookingProcessor(IBookingRepository bookingRepository, IMapper mapper,
            IIdentityService identityService, IAcademicConfigurationRepository academicConfigurationRepository,
            IBedRepository bedRepository, IConfiguration configuration, 
            ILogger<BookingProcessor> logger, IDatabase database, IRoomRepository roomRepository,
            IConnectionMultiplexer redis, RoomProcessor roomProcessor, IHubContext<BookingHub> hubContext, StudentProcessor studentProcessor)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _identityService = identityService;
            _acdemicConfigurationRepository = academicConfigurationRepository;
            _bedRepository = bedRepository;
            _connectionString = $"{configuration["ConnectionStrings:DefaultConnection"]}";
            _logger = logger;
            _database = database;
            _roomRepository = roomRepository;
            _redis = redis;
            _roomProcessor = roomProcessor;
            _hubContext = hubContext;
            _studentProcessor = studentProcessor;
        }



        public async Task<ResponseData> Create(int bedId, int currentVersion)
        {

            var response= new ResponseData();
            var username = _identityService.GetUserName();
           

            var academicPeriod = await _acdemicConfigurationRepository.GetAcademicPeriodAsync();
            

                const string updateBedSql = @"
            UPDATE bed 
            SET BedStatus = @BedStatus, RowVersion = @NewVersion 
            WHERE Id = @BedId AND RowVersion = @CurrentVersion";

                const string updateRoomSql = @"
            UPDATE Room
            SET BookedBeds = BookedBeds + 1
            WHERE Id = @RoomId AND BookedBeds < Capacity;";

                const string insertBookingSql = @"
            INSERT INTO Booking (StudentNumber, BedId, BookingDate, ExpiryDate, ConfirmationDate, AcademicPeriod_LowerYear, AcademicPeriod_UpperYear, AcademicPeriod_Semester, IsVerified,
                Audit_CreatedBy, Audit_Created, Audit_LastModifiedBy, Audit_LastModified, Audit_EntityStatus, Audit_EntityStatusCreated,
                Audit_EntityStatusCreateBy, Audit_EntityStatusLastModified, Audit_EntityStatusLastModifiedBy, OtherProperty, OtherProperty1) 
            VALUES (@StudentNumber, @BedId, @BookingDate, @ExpiryDate, @ConfirmationDate, @AcademicPeriod_LowerYear,
            @AcademicPeriod_UpperYear, @AcademicPeriod_Semester, @IsVerified,
            @Audit_CreatedBy, @Audit_Created,
            @Audit_LastModifiedBy, @Audit_LastModified, 
            @Audit_EntityStatus, @Audit_EntityStatusCreated, @Audit_EntityStatusCreateBy, 
            @Audit_EntityStatusLastModified, @Audit_EntityStatusLastModifiedBy,
            @OtherProperty, @OtherProperty1)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    
                    int newVersion =1; 

                    try
                    {
                        
                        var affectedRows = await connection.ExecuteAsync(updateBedSql, new
                        {
                            BedId = bedId,
                            CurrentVersion = currentVersion,
                            NewVersion = newVersion,
                            BedStatus = BedStatus.Booked,
                        }, transaction);

                        if (affectedRows == 0)
                        {
                            await transaction.RollbackAsync();
                             response = new ResponseData
                            {
                                IsSuccessful = false,
                                Message = "Booking failed: Bed has already been booked",
                                DetailedMessage = ""
                            };
                            
                            _logger.LogInformation("Error: {@ResponseData}", response);

                            return response;

                        }


                        var bed = await connection.QueryFirstOrDefaultAsync<Bed>(
                        "SELECT * FROM Bed WHERE Id = @Id", new { Id = bedId }, transaction);


                        await connection.ExecuteAsync(updateRoomSql, new
                        {
                            RoomId = bed?.RoomId,
                        }, transaction);


                        var booking = new AugmentedBooking
                        {
                            StudentNumber = username,
                            BedId = bedId,
                            BookingDate = DateTime.UtcNow,
                            ExpiryDate= DateTime.UtcNow.AddDays(2),
                            ConfirmationDate=null,
                            AcademicPeriod_LowerYear=academicPeriod.AcademicPeriod.LowerYear,
                            AcademicPeriod_UpperYear=academicPeriod.AcademicPeriod.UpperYear,
                            AcademicPeriod_Semester=academicPeriod.AcademicPeriod.Semester,
                            IsVerified=false,
                            Audit_CreatedBy= username,
                            Audit_Created=DateTime.UtcNow,
                            Audit_LastModifiedBy = username,
                            Audit_LastModified = DateTime.UtcNow,
                            Audit_EntityStatus = EntityStatus.Normal.ToString(),
                            Audit_EntityStatusCreated= DateTime.UtcNow,
                            Audit_EntityStatusCreateBy= username,
                            Audit_EntityStatusLastModified= DateTime.UtcNow,
                            Audit_EntityStatusLastModifiedBy= username,
                            OtherProperty= null,
                            OtherProperty1= null
                        };

                        await connection.ExecuteAsync(insertBookingSql, booking, transaction);
                        await transaction.CommitAsync();

                        response = new ResponseData
                        {
                            IsSuccessful = true,
                            Message = $"{username} booked bed {bed?.BedNumber}, room {bed?.RoomId} successgully",
                            DetailedMessage = ""
                        };


                        //background
                        var key = RedisKeys.GetRooms();
                        var rooms = await _roomProcessor.GetRoomsFromCache(key);
                        if (rooms.Count > 0)
                        {
                            var iconRoom = rooms.Where(a=>a.Id== bed?.RoomId).FirstOrDefault();
                            iconRoom.SlotLeft--;
                            iconRoom.Beds = iconRoom.Beds.Select(a =>
                            {
                                if (a.Id == bedId)
                                {
                                    a.BedStatus = BedStatus.Booked; 
                                }
                                return a; 
                            }).ToList();

                            var settings = new JsonSerializerSettings
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                            };

                            var roomJson = JsonConvert.SerializeObject(iconRoom, settings);
                            await _database.HashSetAsync(key, iconRoom.Id.ToString(), roomJson);

                            await _hubContext.Clients.All.SendAsync("BedBooked", bed.RoomId, bedId);
                            //var bookingInfo = new { RoomId = bed.RoomId, BedId = bedId };
                            
                            //var subscriber = _redis.GetSubscriber();
                            //await subscriber.PublishAsync("bed_bookings", JsonConvert.SerializeObject(bookingInfo));
                        }

                        _logger.LogInformation("success: {@ResponseData}", response);
                        return response;

                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        response = new ResponseData
                        {
                            IsSuccessful = false,
                            Message = $"Booking failed: Network Error",
                            DetailedMessage = $"Booking failed: {ex.Message}"
                        };

                        _logger.LogInformation("Error: {@ResponseData}", response);
                        return response;

                    }
                }
            }
        }

        public async Task<BookingData> GetBooking()
        {
            var bookingdata = new BookingData();

            var refNo = _identityService.GetUserName();
            var student = await _studentProcessor.GetStudentByStudentNumber(refNo);
            if (student == null) return bookingdata;

            var booking = await _bookingRepository.GetUserBooking(refNo);
            if (booking != null)
            {
                bookingdata.IsBooked = true;
                bookingdata.ExpiryDate = booking.ExpiryDate;
                bookingdata.Amount=booking.Bed.Room.RoomType.Price;
                if (booking.ConfirmationDate != null)
                {
                    bookingdata.IsOwned = true;
                }

                var bed = await _bedRepository.GetAsync(booking.BedId);
                if (bed != null)
                {
                    bookingdata.Bed = bed.BedNumber.ToString();
                    bookingdata.Room = bed.Room.RoomNumber.ToString();
                    bookingdata.Hostel = bed?.Room?.Floor?.Building?.Name;
                }

            }

            return bookingdata;
        }


    }

    public class ResponseData
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string DetailedMessage { get; set; }
        
    }

    public class BookingData
    {
        public bool IsBooked { get; set; } = false;
        public bool IsOwned { get; set; } = false;
        public string? Hostel { get; set; }
        public string? Room { get; set; }
        public string? Bed { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public decimal Amount { get; set; }

    }


}
