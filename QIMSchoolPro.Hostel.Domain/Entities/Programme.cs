using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Programme : LookupAuditableEntity<int>
    {

        public ProgrammeCourseRegistrationType ProgrammeCourseRegistrationType { get; private set; }
        public string Code { get; private set; }
        public int? Duration { get; private set; }
        public string IndexNumberPrefix { get; private set; }
        public Certificate Certificate { get; private set; }
        public Department Department { get; set; }

        public string FullName => $"{Certificate?.Code} - {Name} ({Code})/{Department?.Faculty?.SchoolCentre?.Campus?.Name}/{Duration} years";

        private Programme() { }//EF Core

        public Programme(
            ProgrammeCourseRegistrationType programmeCourseRegistrationType,
            string code, string name, string description, Department department,
            int? duration, string indexNumberPrefix, Certificate certificate)
        {
            ProgrammeCourseRegistrationType = programmeCourseRegistrationType;
            Code = code;
            Name = name;
            Description = description;
            Duration = duration;
            IndexNumberPrefix = indexNumberPrefix;
            Certificate = certificate;
            Department = department;
        }


        public static Programme Create(
            ProgrammeCourseRegistrationType programmeCourseRegistrationType,
            string code,
            string name,
            string description,
            Department department,
            int? duration,
            string indexNumberPrefix,
            Certificate certificate)
        {
            return new Programme(programmeCourseRegistrationType,
                             code,
                             name,
                             description,
                             department,
                             duration,
                             indexNumberPrefix,
                             certificate);

        }
        public void Update(
            ProgrammeCourseRegistrationType programmeCourseRegistrationType,
            string code,
            string name,
            string description,
            Department department,
            int? duration,
            string indexNumberPrefix,
            Certificate certificate)
        {
            ProgrammeCourseRegistrationType = programmeCourseRegistrationType;
            Code = code;
            Name = name;
            Description = description;
            Duration = duration;
            IndexNumberPrefix = indexNumberPrefix;
            Certificate = certificate;
            Department = department;

        }


    }
}
