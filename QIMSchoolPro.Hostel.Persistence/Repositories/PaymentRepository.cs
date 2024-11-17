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
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        public ILogger<Payment> Logger { get; }

        public PaymentRepository(HostelDbContext context, ILogger<Payment> logger) : base(context)
        {
            Logger = logger;
        }

        public async Task<List<Payment>> GetPaymentByPartyId(int partyId)
        {
            var data = GetBaseQuery().Where(a => a.PartyId == partyId).ToList();
            return data;
        }

        public override IQueryable<Payment> GetBaseQuery()
        {
            return base.GetBaseQuery();


        }

    }
}
