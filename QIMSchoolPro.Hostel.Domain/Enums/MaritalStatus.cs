using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Enums
{
 
    public enum MaritalStatus
    {
        Single = 1,
        Married,
        Divorce,
        [Description("Unknown")]
        Unknown = 999,

    
    }
}
