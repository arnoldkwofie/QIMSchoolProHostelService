using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class SchoolCentre : LookupAuditableEntity<int>
    {
        public Campus Campus { get; set; }

        private SchoolCentre() { }


        private SchoolCentre(string name, Campus campus, string description = null)
        {
            Name = name;
            Description = description;
            Campus = campus;
        }
        public static SchoolCentre Create(string name, Campus campus,
            string description = null)
        {
            return new SchoolCentre(name, campus,
                description);
        }
        public void Update(string name,
            string description)
        {
            Name = name;
            Description = description;
        }
    }
}
