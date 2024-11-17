using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Domain.Shared.ValueObjects
{
    public class PaymentToken : ValueObjectType
    {
        public string? ReferenceNumber { get; set; }
        public string? PinCode { get; set; }
        public DateTime Date { get; set; }
        public Guid? Id { get; set; }
    }
}
