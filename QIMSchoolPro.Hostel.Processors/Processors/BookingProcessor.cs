using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Qface.Application.Shared.Common.Interfaces;
using Qface.Domain.Shared.Enums;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Exceptions;
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



        public BookingProcessor(IBookingRepository bookingRepository, IMapper mapper,
            IIdentityService identityService, IAcademicConfigurationRepository academicConfigurationRepository,
            IBedRepository bedRepository, IConfiguration configuration, ILogger<BookingProcessor> logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _identityService = identityService;
            _acdemicConfigurationRepository = academicConfigurationRepository;
            _bedRepository = bedRepository;
            _connectionString = $"{configuration["ConnectionStrings:DefaultConnection"]}";
            _logger = logger;
        }



        public async Task<ResponseData> Create(int bedId, int currentVersion)
        {

            var response= new ResponseData();
            //var username = _identityService.GetUserName();
            var username = "9013392023";

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
                            Audit_EntityStatus = EntityStatus.Normal,
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



    }

    public class ResponseData
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public string DetailedMessage { get; set; }
        
    }




}
