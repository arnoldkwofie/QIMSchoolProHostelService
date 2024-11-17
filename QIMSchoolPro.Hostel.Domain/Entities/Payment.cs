using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Payment : AggregateRoot<Guid>
    {
        private Payment(int partyId)
        {
            PartyId = partyId;
            SetId();
        }

        public Payment()
        {
        }


        public int? PartyId { get; private set; }
        public Party Party { get; private set; }
        public PaymentMode PaymentMode { get; private set; }
        public string? BatchNumber { get; private set; }
        public DateTime PaymentDate { get; private set; }

        public Payee Payee { get; set; }
        public PaymentVendorDetail PaymentVendorDetail { get; set; }
        public string Description { get; private set; }
        public PaymentVendor PaymentVendor { get; private set; }
        public int PaymentVendorId { get; private set; }

        public AcademicPeriod AcademicPeriod { get; private set; }

        public string ReceiptNo { get; set; }
        public decimal NetAmount => _paymentItemLines.Sum(a => a.Amount.Value ?? 0);

        private readonly List<PaymentItemLine> _paymentItemLines = new List<PaymentItemLine>();
        public IReadOnlyList<PaymentItemLine> PaymentItemLines => _paymentItemLines.AsReadOnly();

        //public Payment AddItem(int itemId, decimal amount, int quantity)
        //{
        //	_paymentItemLines.Add(PaymentItemLine.Create(itemId)
        //								.WithAmount(amount)
        //								.WithQuantity(quantity)
        //								.WithToken()
        //								.SetId()
        //								);
        //	return this;
        //}

        public Payment AddItems(List<PaymentBreakDown> items)
        {
            var paymentItemLines = new List<PaymentItemLine>();

            foreach (var item in items)
            {
                var paymentItemLine = PaymentItemLine.Create(item.ItemId)
                    .WithAmount(item.Amount)
                    .WithQuantity(item.Quantity)
                    .WithToken()
                    .SetId();

                paymentItemLines.Add(paymentItemLine);
            }
            _paymentItemLines.AddRange(paymentItemLines);

            return this;
        }

        public static Payment Create(int partyId)
        {
            return new Payment(partyId);
        }
        public static Payment Create()
        {
            return new Payment();
        }

        public Payment WithReceipt(string receiptNo)
        {
            ReceiptNo = receiptNo;
            return this;
        }
        public Payment On(DateTime date)
        {
            PaymentDate = date;
            return this;
        }
        public Payment WithPaymentVendorId(int paymentVendorId)
        {
            PaymentVendorId = paymentVendorId;
            return this;
        }
        public Payment WithBatch(string batch)
        {
            BatchNumber = batch;
            return this;
        }
        public Payment TookPlaceOnBankPremise()
        {
            PaymentMode = PaymentMode.BankPremises;
            return this;
        }

        public Payment TookPlaceOnLine()
        {
            PaymentMode = PaymentMode.EPayment;
            return this;
        }

        public Payment WithPartyId(int partyId)
        {
            PartyId = partyId;
            return this;
        }

        public Payment WithComments(string comments)
        {
            Description = comments;
            return this;
        }
        public Payment SetId()
        {
            Id = Guid.NewGuid();
            return this;
        }


        public Payment WithBankInfo(string bankTransactionId, string bankBranch, string tellerName, string tellerId)
        {
            PaymentVendorDetail = new PaymentVendorDetail
            {
                BankTransactionId = bankTransactionId,
                TallerName = tellerName,
                Branch = bankBranch,
                TallerNumber = tellerId
            };
            return this;

        }

        public Payment By(string paymentBy, string paymentByContactNumber)
        {
            Payee = new Payee
            {
                Name = paymentBy,
                PhoneNumber = paymentByContactNumber
            };
            return this;
        }

        public Payment WithAcademicPeriod(AcademicPeriod academicPeriod)
        {
            AcademicPeriod = academicPeriod;
            return this;
        }
    }

    public class PaymentBreakDown
    {
        public int ItemId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }

    }
}
