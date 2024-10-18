using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class PartyDto
    {
        public bool? IsDisability { get; set; }
        public string DisabilityReason { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }

        public PartyType PartyType { get; set; }

        public NameDto Name { get; set; }
        //public EmailAddressValueTypeDto PrimaryEmailAddress { get; set; }
        //public EmailAddressValueTypeDto OtherEmailAddress { get; set; }

        //public PhoneNumberValueTypeDto PrimaryPhoneNumber { get; set; }
        //public PhoneNumberValueTypeDto OtherPhoneNumber { get; set; }

        //public AddressLineDto AddressLine { get; set; }
        //public List<IdentificationCardDto> IdentificationCards { get; set; }


    }
}
