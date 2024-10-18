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
    public class AcademicConfigurationRepository : Repository<AcademicConfiguration>,IAcademicConfigurationRepository
    {
        public ILogger<AcademicConfiguration> Logger { get; }

        public AcademicConfigurationRepository(HostelDbContext context, ILogger<AcademicConfiguration> logger) : base(context)
        {
            Logger = logger;
        }


        public async Task<AcademicConfiguration> GetAcademicPeriodAsync()
        {
            var data = GetBaseQuery().Where(a => a.Active).FirstOrDefault();
            return data!;
        }

        public override IQueryable<AcademicConfiguration> GetBaseQuery()
        {
            return base.GetBaseQuery();
        }

    }
}
