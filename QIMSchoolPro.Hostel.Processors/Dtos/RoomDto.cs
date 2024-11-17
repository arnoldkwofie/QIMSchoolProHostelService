using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public FloorDto? Floor { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public GenderOption GenderOption { get; set; }
        public RoomType? RoomType { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public int BookedBeds { get; set; }
        public List<BedDto> Beds { get; set; }
    }
}
