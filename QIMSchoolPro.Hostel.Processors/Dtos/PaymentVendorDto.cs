using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class PaymentVendorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? BankId { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServerIPAddress { get; set; }
    }
}
