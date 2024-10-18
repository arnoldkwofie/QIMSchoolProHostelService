using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Campus : LookupAuditableEntity<int>
    {
        public static Campus ValidAddTestObject => Create("New Model", "");
        public static Campus InvalidTestObject => Create(null, "");

        //public Location? Location { get; private set; }
        private Campus() { }

        private Campus(
            string name,
            string description
            )
        {
            Name = name;
            Description = description;
        }

        public static Campus Create(string name,
            string description = null)
        {
            return new Campus(
                name,
                description);
        }
        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Campus WithId(int id)
        {
            Id = id;
            return this;
        }
    }
}
