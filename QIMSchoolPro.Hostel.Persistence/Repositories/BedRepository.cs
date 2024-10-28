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
    public class BedRepository : Repository<Bed>, IBedRepository
    {
        public ILogger<Bed> Logger { get; }

        public BedRepository(HostelDbContext context, ILogger<Bed> logger) : base(context)
        {
            Logger = logger;
        }

        


        public override IQueryable<Bed> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a=>a.Room).ThenInclude(a=>a.Floor).ThenInclude(a=>a.Building);
               


        }

    }
}
