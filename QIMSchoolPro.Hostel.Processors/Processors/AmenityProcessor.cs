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
    public class AmenityProcessor
    {
        private readonly IAmenityRepository _amenityRepsotory;
        private readonly IMapper _mapper;

        public AmenityProcessor(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepsotory = amenityRepository;
            _mapper = mapper;

        }
        public async Task Create(AmenityCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);

                var entity = Amenity.Create(command.Name, command.Description);

                await _amenityRepsotory.InsertAsync(entity, cancellationToken);

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


    public class AmenityCommand
    {

        [Required(ErrorMessage = "Room Id is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }

}
