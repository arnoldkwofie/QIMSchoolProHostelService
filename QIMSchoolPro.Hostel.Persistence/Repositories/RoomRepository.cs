using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;


namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public ILogger<Room> Logger { get; }

        public RoomRepository(HostelDbContext context, ILogger<Room> logger) : base(context)
        {
            Logger = logger;
        }

        public async Task<List<Room>> GetRoomsAsync()
        {
            var data =  GetBaseQuery();
            return data.ToList();
        }


        public override IQueryable<Room> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a => a.Floor)
                .ThenInclude(a => a.Building)
                .Include(a => a.RoomType)
                .Include(a => a.Beds);
                
            


        }

    }
}
