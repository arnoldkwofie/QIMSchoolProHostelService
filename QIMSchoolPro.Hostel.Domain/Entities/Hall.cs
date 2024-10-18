using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Hall : AuditableAutoEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public decimal Amount { get; set; }
        public int ItemId { get; set; }
    }
}
