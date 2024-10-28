using AutoMapper;
using Newtonsoft.Json;
using QIMSchoolPro.Hostel.Domain.Constants;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
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
    public class BuildingProcessor
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IRoomTypeAmenityRepository _roomTypeAmenityRepository;
        private readonly IMapper _mapper;
        private readonly IDatabase _database;
        public BuildingProcessor(IBuildingRepository buildingRepository, IRoomTypeAmenityRepository roomTypeAmenityRepository, IMapper mapper, IDatabase database)
        {
            _buildingRepository = buildingRepository;
            _roomTypeAmenityRepository = roomTypeAmenityRepository;
            _mapper = mapper;
            _database = database;
        }
        public async Task Create(BuildingCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);

                Geolocation geolocation = new Geolocation
                {
                    Latitude = command.Latitude,
                    Longitude = command.Longitude,
                };

                var entity = Building.Create(command.Name,
                    command.OwnerShipType, command.GenderOption, geolocation, command.Address, command.Description, command.PicturePath,
                    command.BuildingType);

                await _buildingRepository.InsertAsync(entity, cancellationToken);

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

        //public async Task<List<BuildingDto>> GetAllAsync()
        //{
        //    try
        //    {
        //        var data = await _buildingRepository.GetAllAsync();
        //        return _mapper.Map<List<BuildingDto>>(data);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<List<BuildingViewModel>> GetHostels()
        {
            try
            {

                var key = RedisKeys.GetHostels();
                var buildings = await GetHostelsFromCache(key);


                if (buildings.Count <= 0)
                {
                    var allBuildings = await _buildingRepository.GetAllAsync();
                    var sortedBuildings = allBuildings.Where(a => a.BuildingType == BuildingType.Hostel);

                    foreach (var building in allBuildings)
                    {
                        var roomTypeId = building.Floors?.FirstOrDefault()?.Rooms?.FirstOrDefault()?.RoomTypeId ?? 0;
                        List<string> amenities = new List<string>();
                        if (roomTypeId > 0)
                        {
                            var roomTypeAmenities = await _roomTypeAmenityRepository.GetAmenitiesByRoomType(roomTypeId);
                            foreach (var amenity in roomTypeAmenities)
                            {
                                amenities.Add(amenity.Amenity.Name);
                            }
                        }

                        buildings.Add(
                            new BuildingViewModel
                            {
                                Address = building.Address,
                                Id = building.Id,
                                Name = building.Name,
                                Picture = building.PicturePath,
                                Amenities = amenities,
                                Reviews = "4.5/5 (500 reviews)",
                                PriceRange = "3000 - 5000"
                            });
                    }

                    await AddBuildingsToRedis(key, buildings);
                }

                return buildings;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<BuildingViewModel>> GetHostelsFromCache(string key)
        {
            HashEntry[] buildingHash = await _database.HashGetAllAsync(key);

            List<BuildingViewModel> buildings = new List<BuildingViewModel>();

            foreach (var hashEntry in buildingHash)
            {
                string jsonValue = hashEntry.Value;

                BuildingViewModel building = JsonConvert.DeserializeObject<BuildingViewModel>(jsonValue);

                buildings.Add(building);
            }

            return buildings;
        }

        public async Task AddBuildingsToRedis(string key, List<BuildingViewModel> buildings)
        {

            foreach (var building in buildings)
            {
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };

                var bookJson = JsonConvert.SerializeObject(building, settings);
                await _database.HashSetAsync(key, building.Id.ToString(), bookJson);

            }
            await _database.KeyExpireAsync(key, TimeSpan.FromDays(30));
        }



    }


    public class BuildingCommand
    {
        [Required(ErrorMessage = "Building name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "OwnerShip is required.")]
        public OwnershipType OwnerShipType { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public GenderOption GenderOption { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Picture is required.")]
        public string PicturePath { get; set; }

        [Required(ErrorMessage = "Building type is required.")]
        public BuildingType BuildingType { get; set; }
    }

    public class BuildingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
        public string PriceRange { get; set; }
        public string Reviews { get; set; }
        public List<string> Amenities { get; set; }
    }
}
