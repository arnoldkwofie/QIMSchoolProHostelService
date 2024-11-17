using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Domain.Shared.ValueObjects
{
    public class PaymentVendorDetail : ValueObjectType
    {
        public string? TallerNumber { get; set; }
        public string? TallerName { get; set; }
        public string? Branch { get; set; }
        public string? BankTransactionId { get; set; }

    }

    public class Payee : ValueObjectType
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
