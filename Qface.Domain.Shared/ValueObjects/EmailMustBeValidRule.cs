using System.Text.RegularExpressions;
using Qface.Domain.Shared.Interfaces;

namespace Qface.Domain.Shared.ValueObjects
{
    public partial class EmailMustBeValidRule : IBusinessRule
    {
        private string email;

        public EmailMustBeValidRule(string email)
        {
            this.email = email;
        }
        public string Message
        {
            get
            {
                return IsValidEmail().Meesage;
            }
        }

        public bool IsBroken() => !IsValidEmail().Isvalid;


        internal (bool Isvalid, string Meesage) IsValidEmail()
        {
            if (string.IsNullOrWhiteSpace(email)) return (false, "Email should not be empty");

            email = email.Trim();

            if (email.Length > 200) return (false, "Email is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return (false, "Email is invalid");

            return (true, "");
        }
    }
}