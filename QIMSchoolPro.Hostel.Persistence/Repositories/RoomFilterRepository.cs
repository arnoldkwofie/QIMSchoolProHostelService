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
using QIMSchoolPro.Hostel.Persistence.Migrations;
using RoomFilter = QIMSchoolPro.Hostel.Domain.Entities.RoomFilter;

namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class RoomFIlterRepository : Repository<RoomFilter>, IRoomFilterRepository
    {
        public ILogger<RoomFilter> Logger { get; }

        public RoomFIlterRepository(HostelDbContext context, ILogger<RoomFilter> logger) : base(context)
        {
            Logger = logger;
        }


        public async Task<List<RoomFilter>> GetByRoomId(int roomId)
        {
            var roomFilter = GetBaseQuery().Where(a => a.RoomId==roomId).ToList();
            return roomFilter;
        }


        public override IQueryable<RoomFilter> GetBaseQuery()
        {
            return base.GetBaseQuery();
        }

    }
}
