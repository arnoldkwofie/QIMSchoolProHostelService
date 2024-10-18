using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Entities
{
    public class Student : AuditableEntity
    {
      
        public string StudentNumber { get; set; }
        public string? IndexNumber { get; set; }
        public YearGroup YearGroup { get; set; }
        public StudentStatus? StudentStatus { get; set; }
        public string? StudentType { get; set; }
        public StudentSection StudentSection { get; set; }
        public Guardian Guardian { get; set; }



        public StudentProgrammeCategory? StudentProgrammeCategory { get; set; }
        public string? UserId { get; set; }

        public Programme Programme { get; set; }
        //public int ProgrammeId { get; private set; }
        public int ProgrammeId { get; set; }
        public long? ApplicantId { get; set; }
        public int? HallId { get; set; }
        public Hall Hall { get; set; }
        public int? AffiliatedHallId { get; set; }
        public string? CountryId { get; set; }

        public Student() { }//For EF Core


        public Student(Party party, string studentNumber,
            string indexNumber,
            int yearGroup, string studentType)
        {
            Party = party;
            StudentNumber = studentNumber;
            IndexNumber = indexNumber;
            YearGroup = YearGroup.Create(yearGroup);
            StudentType = studentType;
        }

        public Student(string studentNumber)
        {
            StudentNumber = studentNumber;
        }



        public Party Party { get; set; }
        public int PartyId { get; set; }

        public Student WithParty(Party party)
        {
            Party = party;
            return this;
        }
        public Student WithStudentNo(string studentNo)
        {
            StudentNumber = studentNo;
            return this;
        }
        public Student WithIndexNo(string indexNo)
        {
            IndexNumber = indexNo;
            return this;
        }
        public Student WithYearGroup(YearGroup yearGroup)
        {
            YearGroup = yearGroup;
            return this;
        }
        public Student SetStatus(StudentStatus status)
        {
            StudentStatus = status;
            return this;
        }



        //Ignore
        public string PhotoUrl { get; set; }

        public int Year { get; set; }
        public bool HasCompleteSchool { get; set; }



        #region ReadOnlyList



      

        public static Student Create(Party party, string studentNumber, string indexNumber, int yearGroup, string studentType)
        {
            return new Student(party, studentNumber, indexNumber, yearGroup, studentType);
        }

        public static Student Create(string studentNumber)
        {
            return new Student(studentNumber);
        }

        public Student Update(Party party, string studentNumber, string indexNumber, YearGroup yearGroup, string studentType, StudentStatus studentStatus,
          StudentSection studentSection, Guardian guardian, StudentProgrammeCategory studentProgrammeCategory, int programmeId,
          string photoUrl)
        {
            Party = party;
            StudentNumber = studentNumber;
            IndexNumber = indexNumber;
            YearGroup = yearGroup;
            StudentType = studentType;
            StudentStatus = studentStatus;
            StudentSection = studentSection;
            Guardian = guardian;
            StudentProgrammeCategory = studentProgrammeCategory;
            ProgrammeId = programmeId;
            PhotoUrl = photoUrl;


            return this;
        }

        public Student SetStudentCurrentLevel(AcademicConfiguration academicConfiguration, int classYear)
        {

            var realLevel = academicConfiguration.AcademicPeriod.UpperYear - classYear;
            int actualLevel = (realLevel > 4 ? 400 : realLevel * 100);

            Year = actualLevel;
            return this;

        }
        public Student SetIsStudentDoneWithSchool(AcademicConfiguration academicConfiguration, int classYear)
        {
            var realLevel = academicConfiguration.AcademicPeriod.UpperYear - classYear;
            if (realLevel > 400)
                HasCompleteSchool = true;

            if (realLevel == 400 && academicConfiguration.AcademicPeriod.Semester == Semester.SecondSemester &&
                academicConfiguration.OngoingActivity.ShowResult)
                HasCompleteSchool = true;

            HasCompleteSchool = false;

            return this;

        }
        #endregion


    }
}
