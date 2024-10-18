using Qface.Domain.Shared.Common;
using Qface.Domain.Shared.Enums;

namespace Qface.Domain.Shared.ValueObjects
{

	public class Email : ValueObjectType
	{
		public EmailType EmailType { get;  set; }

		public string? Value { get;  set; }

		 Email() { }
		 Email(string value, EmailType type)
		{
			Value = value;
			EmailType = type;
			CheckRule(new EmailMustBeValidRule(value));
		}
		public Email WithEmail(string email)
		{
			Value = email;
			return this;
		}
		public static Email Create(string email, EmailType type = EmailType.Other) => new Email(email, type);



		public static implicit operator string(Email email)
		{
			return email.Value;
		}
	}
}
