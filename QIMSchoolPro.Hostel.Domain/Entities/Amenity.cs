using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Amenity:AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public Amenity()
        {
                
        }

        public Amenity(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public static Amenity Create(string name, string? description)
        {
            return new Amenity(name, description);
        }
    }
}
