using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Dtos
{
    public class StudentDto
    {
        public string StudentNumber { get; set; }
        public string IndexNumber { get; set; }
        public YearGroup YearGroup { get; set; }
        public StudentStatus? StudentStatus { get; set; }
        public string StudentType { get; set; }
        public StudentSection StudentSection { get; set; }
        
        public StudentProgrammeCategory? StudentProgrammeCategory { get; set; }
        public string UserId { get; set; }
        public PartyDto Party { get; set; }
        public int PartyId { get; set; }
        public ProgrammeDto Programme { get; set; }
        public int ProgrammeId { get; set; }
        
        public int Year { get; set; }
        public int CalculatedYear => (DateTime.Now.Year - YearGroup.ClassYear) > 1 ? (DateTime.Now.Year - YearGroup.ClassYear) * 100 : 100;
        public bool HasCompleteSchool { get; set; }

        public string PhotoUrl { get; set; }
        public int? HallId { get; set; }
        public HallDto Hall { get; set; }



    }

}
