using Qface.Application.Shared.Dtos;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class PaymentDto
    {
        public int? PartyId { get; set; }
        public PartyDto Party { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public string BatchNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TallerNumber { get; set; }
        public string TallerName { get; set; }
        public string Branch { get; set; }
        public decimal NetAmount { get; set; }
        public string BankTransactionId { get; set; }
        public string Description { get; set; }
        public Payee Payee { get; set; }
        public PaymentVendorDto PaymentVendor { get; set; }
        public int PaymentVendorId { get; set; }

        public AcademicPeriod AcademicPeriod { get; set; }

        public PaymentVendorDetail PaymentVendorDetail { get; set; }
    }
}
