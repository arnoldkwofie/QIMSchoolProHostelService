using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Bed: AuditableEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int BedNumber { get; set; }
        public BedType BedType { get; set; }
        public BedPosition BedPosition { get; set; }
        public BedStatus BedStatus { get; set; }
        public string Description { get; set; }
        public int RowVersion { get; set; }

        public Bed()
        {
                
        }
        public Bed(int roomId, int bedNumber, BedType bedType, BedPosition bedPosition, string description, int rowVersion)
        {
            RoomId = roomId;
            BedNumber = bedNumber;
            BedType = bedType;
            BedPosition = bedPosition;
            Description = description;
            RowVersion = rowVersion;

        }

        public static  Bed Create(int roomId, int bedNumber, BedType bedType, BedPosition bedPosition, string description, int rowVersion)
        {
           return new Bed(roomId, bedNumber, bedType, bedPosition, description, rowVersion);
        }
    }
}
