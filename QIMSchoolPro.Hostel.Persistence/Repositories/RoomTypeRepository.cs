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
    public class RoomTypeRepository : Repository<RoomType>, IRoomTypeRepository
    {
        public ILogger<Floor> Logger { get; }

        public RoomTypeRepository(HostelDbContext context, ILogger<Floor> logger) : base(context)
        {
            Logger = logger;
        }


        public override IQueryable<RoomType> GetBaseQuery()
        {
            return base.GetBaseQuery();


        }

    }
}
