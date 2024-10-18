using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Room: AuditableEntity
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public Floor Floor { get; set; }
        public int RoomNumber { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public int BookedBeds { get; set; }
        public List<Bed> Beds { get; set; }

        public Room()
        {
                
        }

        public Room(int floorId, int roomNumber, int roomTypeId, int capacity, string? description)
        {
            FloorId= floorId;   
            RoomNumber= roomNumber;
            RoomTypeId= roomTypeId;
            Capacity= capacity;
            Description= description;

        }

        public static Room create(int floorId, int roomNumber, int roomTypeId, int capacity, string? description)
        { return new Room(
            floorId,roomNumber, roomTypeId, capacity,description
            ); 
        }
    }
}
