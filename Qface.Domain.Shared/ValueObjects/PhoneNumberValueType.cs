using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;

namespace Qface.Domain.Shared.ValueObjects
{
	public class PhoneNumberValueType : ValueObjectType
	{
		public PhoneNumberType PhoneNumberType { get; private set; }
		public Phone Phone { get; private set; }
		private PhoneNumberValueType() { }//EF Core
		private PhoneNumberValueType(PhoneNumberType phoneNumberType, Phone phone)
		{
			PhoneNumberType = phoneNumberType;
			Phone = phone;
		}

		public static PhoneNumberValueType Create(string number, string countryId,
			PhoneNumberType phoneNumberType = PhoneNumberType.Other)
		{
			return new PhoneNumberValueType(phoneNumberType, Phone.Create(number, countryId));
		}
		public static PhoneNumberValueType Create(string number,
			PhoneNumberType phoneNumberType = PhoneNumberType.Other)
		{
			return new PhoneNumberValueType(phoneNumberType, Phone.Create(number));
		}
	}


}
