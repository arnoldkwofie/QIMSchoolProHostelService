using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class RoomTypeAmenity:AuditableEntity
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
        public bool Availabilty { get; set; }

        public RoomTypeAmenity()
        {
                
        }

        public RoomTypeAmenity(int roomTypeId, int amenityId, bool availability)
        {
            RoomTypeId = roomTypeId;
            AmenityId= amenityId;
            Availabilty = availability;
        }

        public static RoomTypeAmenity Create(int roomTypeId, int amenityId, bool availability)
        {
            return new RoomTypeAmenity(roomTypeId, amenityId, availability);
        }
    }
}
