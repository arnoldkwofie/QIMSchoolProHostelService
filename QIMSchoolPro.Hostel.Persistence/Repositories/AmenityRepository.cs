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
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        public ILogger<Amenity> Logger { get; }

        public AmenityRepository(HostelDbContext context, ILogger<Amenity> logger) : base(context)
        {
            Logger = logger;
        }

    


        public override IQueryable<Amenity> GetBaseQuery()
        {
            return base.GetBaseQuery();
        }

    }
}
