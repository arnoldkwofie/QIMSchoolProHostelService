using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class RoomFilter: AuditableEntity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public RoleSection Section { get; set; }
        public int? OfficeId { get; set; }
    }
}
