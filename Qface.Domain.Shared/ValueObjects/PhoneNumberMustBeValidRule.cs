using Qface.Domain.Shared.Interfaces;

namespace Qface.Domain.Shared.ValueObjects
{
    public class PhoneNumberMustBeValidRule : IBusinessRule
    {
        private readonly Phone phone;

        public PhoneNumberMustBeValidRule(Phone phone)
        {
            this.phone = phone;
        }
        public string Message => "invalid phone number";

        public bool IsBroken() => IsValidPhoneNumber(phone.Number);
        internal bool IsValidPhoneNumber(string phoneNumber)
        {
            return !(string.IsNullOrEmpty(phoneNumber) || string.IsNullOrWhiteSpace(phoneNumber));
        }
    }
}