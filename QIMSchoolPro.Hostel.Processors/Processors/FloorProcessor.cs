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
    public class FloorProcessor
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IMapper _mapper;

        public FloorProcessor(IFloorRepository floorRepository, IMapper mapper)
        {
            _floorRepository = floorRepository;
            _mapper=mapper;
           
        }
        public async Task Create(FloorCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);
               

                var entity = Floor.Create(command.BuildingId, command.FloorNumber, command.Description);

                await _floorRepository.InsertAsync(entity, cancellationToken);

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


    public class FloorCommand
    {

        [Required(ErrorMessage = "Building Id is required.")]
        public int BuildingId { get; set; }
        [Required(ErrorMessage = "Floor Number is required.")]
        public FloorNumber FloorNumber { get; set; }
        public string? Description { get; set; }
    }
}
