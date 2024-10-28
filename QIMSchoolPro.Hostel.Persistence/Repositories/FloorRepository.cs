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

namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class FloorRepository : Repository<Floor>, IFloorRepository
    {
        public ILogger<Floor> Logger { get; }

        public FloorRepository(HostelDbContext context, ILogger<Floor> logger) : base(context)
        {
            Logger = logger;
        }


        public override IQueryable<Floor> GetBaseQuery()
        {
            return base.GetBaseQuery();


        }

    }
}
