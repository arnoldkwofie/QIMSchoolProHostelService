using Qface.Domain.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class NameDto
    {
        public Sex Sex { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherName { get; set; }

        public int SalutationId { get; set; }

        //public SalutationDto Salutation { get; set; }
        //public LookupDtoBase<Guid> Suffix { get; set; }

        public string FullName { get; set; }
        public string FullNamev2 { get; set; }
        public string FullNamev3 { get; set; }

    }
}
