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
    public class BedProcessor
    {
        private readonly IBedRepository _bedRepository;
        private readonly IMapper _mapper;

        public BedProcessor(IBedRepository bedRepository, IMapper mapper)
        {
            _bedRepository = bedRepository;
            _mapper = mapper;

        }
        public async Task Create(BedCommand command, CancellationToken cancellationToken)
        {
            try
            {
                Validator.ValidateObject(command, new ValidationContext(command), validateAllProperties: true);

                var entity = Bed.Create(command.RoomId, command.BedNumber, command.BedType, command.BedPosition, command.Description, 1);

                await _bedRepository.InsertAsync(entity, cancellationToken);

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

        private async Task<List<BedDto>> GetAllAync()
        {
            var data = await _bedRepository.GetAllAsync();
            var sorted = _mapper.Map<List<BedDto>>(data);
            return sorted.ToList();
        }

        public async Task<List<BedDto>> GetBedsByRoomId(int id)
        {
            var data = await GetAllAync();
            var result = data.Where(a=>a.RoomId==id).ToList();
            return result;
        }

    }


    public class BedCommand
    {

        [Required(ErrorMessage = "Room Id is required.")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Room Id is required.")]
        public int BedNumber { get; set; }
        [Required(ErrorMessage = "Bed type is required.")]
        public BedType BedType { get; set; }
        [Required(ErrorMessage = "Bed position is required.")]
        public BedPosition BedPosition { get; set; }
        public string Description { get; set; }
    }

}
