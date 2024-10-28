using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class BedDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int BedNumber { get; set; }
        public BedType BedType { get; set; }
        public BedPosition BedPosition { get; set; }
        public BedStatus BedStatus { get; set; }
        public string? Description { get; set; }
    }
}
