using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class FloorDto
    {
        public int Id { get; set; }
        public FloorNumber FloorNumber { get; set; }
        public BuildingDto? Building { get; set; }
        public string? Description { get; set; }
        
    }
}
