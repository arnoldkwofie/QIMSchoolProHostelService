using AutoMapper;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Migrations;
using QIMSchoolPro.Hostel.Persistence.Repositories;
using QIMSchoolPro.Hostel.Processors.Dtos;
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

        public RoomProcessor(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;

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



        public async Task<List<RoomViewModel>> GetRoomsByBuildingId(int id)
        {
            try
            {
                var allRooms = await _roomRepository.GetRoomsAsync();
                var sorted = allRooms.Where(a => a.Floor.BuildingId == id);
                var mapped = _mapper.Map<List<RoomDto>>(sorted);

                var result = new List<RoomViewModel>();
                foreach (var item in mapped)
                {
                    result.Add(
                        new RoomViewModel
                        {
                            Capacity = item.Capacity,
                            Hostel = item.Floor?.Building?.Name,
                            Id=item.Id,
                            Price=item.RoomType?.Price,
                            RoomType=item.RoomType?.Name,
                            RoomNumber="R "+item.RoomNumber,
                            SlotLeft=2,
                            Beds=item.Beds,
                            


                        }
                        ); ;
                    
                }
               
                return result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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
        public string? RoomNumber { get; set; }
        public string? RoomType { get; set; }
        public string? Hostel { get; set; }
        public decimal? Price { get; set; }
        public int Capacity { get; set; }
        public int SlotLeft { get; set; }
        public List<BedDto> Beds { get; set; }
    }

}
