using Qface.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class PaymentItemLineDto
    {
        public Money Amount { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int ItemId { get; set; }
        public PaymentTokenDto Token { get; set; }

    }
    public class PaymentTokenDto
    {
        public string ReferenceNumber { get; set; }
        public string PinCode { get; set; }
        public DateTime Date { get; set; }
        public Guid? Id { get; set; }
    }
}
