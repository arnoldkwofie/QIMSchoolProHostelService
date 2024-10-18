using AutoMapper;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
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
    public class RoomTypeAmenityProcessor
    {
        private readonly IRoomTypeAmenityRepository _roomTypeAmenityRepsository;
        private readonly IMapper _mapper;

        public RoomTypeAmenityProcessor(IRoomTypeAmenityRepository roomTypeAmenityRepository, IMapper mapper)
        {
            _roomTypeAmenityRepsository = roomTypeAmenityRepository;
            _mapper = mapper;

        }
        public async Task Create(RommTypeAmenityCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);

                var entity = RoomTypeAmenity.Create(command.RoomTypeId, command.AmenityId, command.Availabilty);

                await _roomTypeAmenityRepsository.InsertAsync(entity, cancellationToken);

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

   

    }


    public class RommTypeAmenityCommand
    {

        [Required(ErrorMessage = "RoomType is required.")]
        public int RoomTypeId { get; set; }
        [Required(ErrorMessage = "Amenity is required.")]
        public int AmenityId { get; set; }
        [Required(ErrorMessage = "Availabity Status is required.")]
        public bool Availabilty { get; set; }
    }

}
