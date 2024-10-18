using Qface.Domain.Shared.Interfaces;

namespace Qface.Domain.Shared.ValueObjects
{
    public class AddressLineMustBeValidRule : IBusinessRule
    {
        private readonly AddressLine address;

        public AddressLineMustBeValidRule(AddressLine address)
        {
            this.address = address;
        }
        public string Message => "invalid address line";

        public bool IsBroken() => !IsValidAddressLines();
        private bool IsValidAddressLines()
        {
            if (string.IsNullOrEmpty(address.Line1) ||
                string.IsNullOrWhiteSpace(address.Line1))
                return false;

            return true;
        }
    }
}