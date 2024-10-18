using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public ILogger<Booking> Logger { get; }

        public BookingRepository(HostelDbContext context, ILogger<Booking> logger) : base(context)
        {
            Logger = logger;
        }


        public override IQueryable<Booking> GetBaseQuery()
        {
            return base.GetBaseQuery();
        }

    }
}
