using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class RoomTypeAmenityRepository : Repository<RoomTypeAmenity>, IRoomTypeAmenityRepository
    {
        public ILogger<RoomTypeAmenity> Logger { get; }

        public RoomTypeAmenityRepository(HostelDbContext context, ILogger<RoomTypeAmenity> logger) : base(context)
        {
            Logger = logger;
        }

        public async Task<List<RoomTypeAmenity>> GetAmenitiesByRoomType(int? id)
        {
            var data = GetBaseQuery().Where(a => a.RoomTypeId==id).ToList();
            return data;
        }


        public override IQueryable<RoomTypeAmenity> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a=>a.Amenity);
               


        }

    }
}
