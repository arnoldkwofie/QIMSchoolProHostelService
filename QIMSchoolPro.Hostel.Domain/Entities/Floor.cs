using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Floor: AuditableEntity
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public Building? Building { get; set; }
        public FloorNumber FloorNumber { get; set; }
        public string? Description { get; set; }

        //[NotMapped]
        public List<Room> Rooms { get; set; }

        public Floor()
        {
                
        }

        public Floor(int buildingId, FloorNumber floorNumber, string? description)
        {
            BuildingId = buildingId;
            FloorNumber = floorNumber;
            Description = description;
        }

        public static Floor Create(int buildingId, FloorNumber floorNumber, string? description)
        {
            return new Floor(buildingId, floorNumber, description);  
        }
    }
}
