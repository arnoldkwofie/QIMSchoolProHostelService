using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.ValueObjects;
using Qface.Extension.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class PaymentItemLine : AuditableEntity<Guid>
    {
        public PaymentItemLine(int itemId)
        {
            this.ItemId = itemId;
        }
        //public Item Item { get; private set; }
        public int ItemId { get; private set; }
        public Money Amount { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total => Quantity * Amount.Value ?? 0;
        public PaymentToken Token { get; set; }
        public Payment Payment { get; private set; }
        public Guid PaymentId { get; private set; }

        public PaymentItemLine SetId()
        {
            Id = Guid.NewGuid();
            return this;
        }
        public static PaymentItemLine Create(int itemId)
        {
            return new PaymentItemLine(itemId);
        }
        public PaymentItemLine WithItemId(int itemId)
        {
            ItemId = itemId;
            return this;
        }
        public PaymentItemLine WithAmount(decimal amount)
        {
            Amount = Money.Of(amount,
                "GHS",
                Rate.Create());
            return this;
        }
        public PaymentItemLine WithQuantity(int quantity)
        {
            Quantity = quantity;
            return this;
        }
        public PaymentItemLine WithToken()
        {
            Token = new PaymentToken
            {
                Date = DateTime.Now,
                PinCode = PinGenerator.GenerateAlphaNumeric(8),
                Id = Guid.NewGuid(),
            };
            return this;
        }

    }
}
