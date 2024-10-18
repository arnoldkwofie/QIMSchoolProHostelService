using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class RoomType: AuditableEntity
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public RoomType()
        {
                
        }

        public RoomType(int buildingId, string name, decimal price)
        {
            BuildingId = buildingId;
            Name = name;
            Price = price;
        }

        public static RoomType Create(int buildingId, string name, decimal price)
        {
            return new RoomType(buildingId, name, price);
        }
    }
}
