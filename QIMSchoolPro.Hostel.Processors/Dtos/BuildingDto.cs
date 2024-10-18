using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class BuildingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OwnershipType OwnerShipType { get; set; }
        public GenderOption GenderOption { get; set; }
        public Geolocation Geolocation { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }
        public BuildingType BuildingType { get; set; }
    }
}
