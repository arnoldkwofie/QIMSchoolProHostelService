using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public async Task<Booking> GetUserBooking(string studentNumber)
        {
            var data = GetBaseQuery().Where(a=>a.StudentNumber == studentNumber).FirstOrDefault();
            return data;
        }

        public override IQueryable<Booking> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a=>a.Bed)
                .ThenInclude(a=>a.Room)
                .ThenInclude(a=>a.RoomType);
        }
    }
}
