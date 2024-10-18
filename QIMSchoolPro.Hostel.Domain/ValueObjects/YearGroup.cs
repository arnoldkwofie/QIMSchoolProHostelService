using Qface.Domain.Shared.Common;

namespace QIMSchoolPro.Hostel.Domain.ValueObjects
{
	public class YearGroup : ValueObjectType
	{

		public YearGroup(int admittedYear)
		{
			AdmittedYear = admittedYear;
		}

		public int AdmittedYear { get; private set; }
		public int ClassYear { get; private set; }

		public int Value
		{
			get
			{
				if (AdmittedYear == ClassYear)
					return AdmittedYear;
				return ClassYear;
			}
		}

		public YearGroup Create(int admittedYear)
		{
			return new YearGroup(admittedYear);
		}

		public YearGroup WithClassYear(int year)
		{
			ClassYear = year;
			return this;
		}
	}

}
