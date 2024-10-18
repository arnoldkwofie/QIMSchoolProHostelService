using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;

namespace Qface.Domain.Shared.ValueObjects
{
	public class EmailAddressValueType : ValueObjectType
	{
		public Email Email { get;  set; }

		private EmailAddressValueType() { } //For EF Core

		private EmailAddressValueType(Email email)
		{
			Email = email;
		}

		public static EmailAddressValueType Create(string email, EmailType type)
		{
			return new EmailAddressValueType(Email.Create(email, type));
		}


	}


}
