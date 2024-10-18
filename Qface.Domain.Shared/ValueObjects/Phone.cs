using Qface.Domain.Shared.Common;


namespace Qface.Domain.Shared.ValueObjects
{
	public class Phone : ValueObjectType
	{
		public string? Number { get; private set; }
		public string? CountryCode { get; private set; }

		private Phone() { }
		private Phone(string number, string countryCode)
		{
			CheckRule(new PhoneNumberMustBeValidRule(this));
			Number = number;
			if (countryCode == null)
				countryCode = "GHA";

			Number = number;
			CountryCode = countryCode;
		}

		public static Phone Create(string number, string countryCode = "233")
		{
			return new Phone(number, countryCode);
		}
		public Phone WithPhoneNumber(string phoneNumber)
		{
			Number = phoneNumber;
			return this;
		}


	}
}
