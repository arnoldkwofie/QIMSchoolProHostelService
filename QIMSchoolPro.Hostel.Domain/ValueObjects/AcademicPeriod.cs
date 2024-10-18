using Qface.Domain.Shared.Common;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;

namespace QIMSchoolPro.Hostel.Domain.ValueObjects
{
	public class AcademicPeriod : ValueObjectType
	{

		public string AcademicYear  => $"{LowerYear}/{UpperYear}";

		public int LowerYear { get; private set; }
		public int UpperYear { get; private set; }
		public Semester Semester { get; private set; }

		private AcademicPeriod() { }//For EF
		private AcademicPeriod(string academicYear, Semester semester)
		{
			var yearParts = academicYear.Split('/');
			LowerYear = Convert.ToInt32(yearParts[0]);
			UpperYear = Convert.ToInt32(yearParts[1]);
			Semester = semester;
		}

		public void Validate()
		{
			var invalid = LowerYear < 1000 || UpperYear < 1000;

			if (invalid)
				throw new Exception("Invalid academic period");
		}

		public static AcademicPeriod Create(string academicYear, Semester semester)
		{
			return new AcademicPeriod(academicYear, semester);
		}

		
		public bool IsSameAcademicPeriod(AcademicPeriod academicPeriod)
		{
			return LowerYear == academicPeriod.LowerYear
				&& UpperYear == academicPeriod.UpperYear && academicPeriod.Semester == Semester;
		}



		public static (int Lower, int Upper) ExtractAcademicYear(string academicYear)
		{
			var yearParts = academicYear.Split('/');
			return (Convert.ToInt32(yearParts[0]), Convert.ToInt32(yearParts[1]));
		}
	}

}
