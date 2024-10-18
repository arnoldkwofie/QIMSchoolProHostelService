using Humanizer;
using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;


namespace QIMSchoolPro.Hostel.Domain.ValueObjects
{
	public class Name : ValueObjectType
	{
		public Sex Sex { get; private set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? OtherName { get; set; }

		public string FullNamev3 => GetFullName();

        private string GetFullName()
        {
            if (string.IsNullOrWhiteSpace(OtherName))
                return $"{LastName?.ToUpper()}, {FirstName?.ToLower().Humanize(LetterCasing.Title)}".Trim();
            return $"{LastName?.ToUpper()}, {FirstName?.ToLower().Humanize(LetterCasing.Title)} {OtherName?.ToLower().Humanize(LetterCasing.Title)}".Trim();
        }

    }

}
