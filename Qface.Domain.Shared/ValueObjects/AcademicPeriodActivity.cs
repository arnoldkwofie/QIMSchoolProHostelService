using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qface.Domain.Shared.ValueObjects
{
    public class AcademicPeriodActivity : ValueObjectType
        {
            public bool SemesterRegistration { get; set; }
            public bool LecturerAssessment { get; set; }
            public bool ShowResult { get; set; }
    }
}
