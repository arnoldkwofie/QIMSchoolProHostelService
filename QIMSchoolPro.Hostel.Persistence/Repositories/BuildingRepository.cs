using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;


namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class BuildingRepository : Repository<Building>, IBuildingRepository
    {
        public ILogger<Building> Logger { get; }

        public BuildingRepository(HostelDbContext context, ILogger<Building> logger) : base(context)
        {
            Logger = logger;
        }

      

        public override IQueryable<Building> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a=>a.Floors).ThenInclude(a=>a.Rooms);


        }

    }
}
