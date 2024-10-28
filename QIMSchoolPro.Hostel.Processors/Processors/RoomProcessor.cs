using AutoMapper;
using Newtonsoft.Json;
using QIMSchoolPro.Hostel.Domain.Constants;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Migrations;
using QIMSchoolPro.Hostel.Persistence.Repositories;
using QIMSchoolPro.Hostel.Processors.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QIMSchoolPro.Hostel.Processors.Processors
{
    [ProcessorBase]
  
    public class RoomProcessor
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _database;
        private readonly IRoomFilterRepository _roomFilterRepository;
        private readonly IStudentRepository _studentRepository;
        public RoomProcessor(IRoomRepository roomRepository, IMapper mapper, IDatabase database,
            IRoomFilterRepository roomFilterRepository, IStudentRepository studentRepository)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _database = database;
            _roomFilterRepository = roomFilterRepository;
            _studentRepository = studentRepository;
        }
        public async Task Create(RoomCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);

                var entity = Room.create(command.FloorId, command.RoomNumber, command.RoomTypeId, command.Capacity, command.Description);

                await _roomRepository.InsertAsync(entity, cancellationToken);

            }
            catch (ValidationException validationEx)
            {
                throw new ValidationException($"Validation failed: {validationEx.Message}");
            }
            catch (Exception ex)
            {
                throw new ValidationException($"An error occurred while creating: {ex.Message}");
            }
        }

        public async Task<List<RoomViewModel>> GetRoomsByHostelId(int id)
        {
            try
            {
                //var username = _identityService.GetUserName();
                var username = "9013392023";

                var key = RedisKeys.GetRooms();
                var rooms = await GetRoomsFromCache(key);

                if (rooms.Count <= 0)
                {
                   var allRooms = await _roomRepository.GetRoomsAsync();
                    allRooms.ForEach(room => room.Floor.Rooms = null);
                    var mapped = _mapper.Map<List<RoomDto>>(allRooms);

                    rooms = new List<RoomViewModel>();
                    foreach (var item in mapped)
                    {
                        rooms.Add(
                        new RoomViewModel
                        {
                            Capacity = item.Capacity,
                            Hostel = item.Floor?.Building?.Name,
                            Id = item.Id,
                            Price = item.RoomType?.Price,
                            RoomType = item.RoomType?.Name,
                            RoomNumber = "R " + item.RoomNumber,
                            SlotLeft = item.Capacity - item.BookedBeds < 0 ? 0 : item.Capacity - item.BookedBeds,
                            Beds = item.Beds,
                            HostelId=item?.Floor?.Building?.Id,
                        }
                        );
                    }
                    await AddRoomsToRedis(key, rooms);
                }

                var sorted = rooms.Where(a => a.HostelId == id && a.SlotLeft >0).OrderBy(a=>a.Id).ToList();
                var advanceFilter = await FilterRoom(sorted, username);

                return advanceFilter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<RoomViewModel>> FilterRoom(List<RoomViewModel> rooms, string studentNumber)
        {
            var student = await _studentRepository.GetAsync(studentNumber);
            var filteredRooms = new List<RoomViewModel>();

            foreach (var room in rooms)
            {
              

                var roomFilter = await _roomFilterRepository.GetByRoomId(room.Id);
                bool isEligible = true;

                foreach (var filter in roomFilter)
                {
                    switch (filter.Section)
                    {
                        case RoleSection.Level:
                            if (filter.OfficeId != (DateTime.Now.Year - student.YearGroup.ClassYear) * 100)
                            {
                                isEligible = false;
                            }
                            break;

                        case RoleSection.Campus:
                            if (filter.OfficeId != student.Programme.Department.Faculty.SchoolCentre.Campus.Id)
                            {
                                isEligible = false;
                            }
                            break;

                        case RoleSection.SchoolCentre:
                            if (filter.OfficeId != student.Programme.Department.Faculty.SchoolCentreId)
                            {
                                isEligible = false;
                            }
                            break;

                        case RoleSection.Faculty:
                            if (filter.OfficeId != student.Programme.Department.FacultyId)
                            {
                                isEligible = false;
                            }
                            break;

                        case RoleSection.Department:
                            if (filter.OfficeId != student.Programme.Department.Id)
                            {
                                isEligible = false;
                            }
                            break;

                        case RoleSection.Programme:
                            if (filter.OfficeId != student.ProgrammeId)
                            {
                                isEligible = false;
                            }
                            break;
                    }

                    if (!isEligible) break; 
                }

                if (isEligible)
                {
                    filteredRooms.Add(room);
                }
            }

            return filteredRooms;
        }



        public async Task<List<RoomViewModel>> GetRoomsFromCache(string key)
        {
            HashEntry[] roomHash = await _database.HashGetAllAsync(key);

            List<RoomViewModel> rooms = new List<RoomViewModel>();

            foreach (var hashEntry in roomHash)
            {
                string jsonValue = hashEntry.Value;
                RoomViewModel room = JsonConvert.DeserializeObject<RoomViewModel>(jsonValue);
                rooms.Add(room);
            }
            return rooms;
        }

        public async Task AddRoomsToRedis(string key, List<RoomViewModel> rooms)
        {
            foreach (var room in rooms)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                var bookJson = JsonConvert.SerializeObject(room, settings);
                await _database.HashSetAsync(key, room.Id.ToString(), bookJson);
            }
            await _database.KeyExpireAsync(key, TimeSpan.FromDays(30));
        }
    }

    public class RoomCommand
    {
        [Required(ErrorMessage = "Floor is required.")]
        public int FloorId { get; set; }
        [Required(ErrorMessage = "RoomNumber is required.")]
        public int RoomNumber { get; set; }
        [Required(ErrorMessage = "RoomType Id is required.")]
        public int RoomTypeId { get; set; }
        [Required(ErrorMessage = "Capacity is required.")]
        public int Capacity { get; set; }
        public string? Description { get; set; }
    }

    public class RoomViewModel
    {
        public int Id { get; set; }
        public int? HostelId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomType { get; set; }
        public string? Hostel { get; set; }
        public decimal? Price { get; set; }
        public int Capacity { get; set; }
        public int SlotLeft { get; set; }
        public List<BedDto> Beds { get; set; }
    }

}
