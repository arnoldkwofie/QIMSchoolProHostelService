using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace Qface.Extension.Shared
{
    public class PinGenerator
    {
        // Define default password length.
        private const int DefaultPasswordLength = 5;

        //No characters that are confusing: i, I, l, L, o, O, 0, 1, u, v

        public static string PasswordCharsAlpha = "abcdefghjkmnpqrstwxyzABCDEFGHJKMNPQRSTWXYZ";
        public static string PasswordCharsNumeric = "23456789";
        public static string PasswordCharsSpecial = "*$-+?_&=!%{}/";
        //public static string PasswordCharsSpecial = "";
        public static string PasswordCharsAlphanumeric =
                                PasswordCharsAlpha + PasswordCharsNumeric;
        public static string PasswordCharsAll = PasswordCharsAlphanumeric + PasswordCharsSpecial;

        //These overloads are only necesary in versions of .NET below 4.0
        #region Overloads

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate()
        {
            return Generate(DefaultPasswordLength, PasswordCharsAll);
        }

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string GenerateAlphaNumeric(int length)
        {
            return Generate(length, PasswordCharsAlphanumeric);
        }

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(string passwordChars)
        {
            return Generate(DefaultPasswordLength,
                            passwordChars);
        }

        /// <summary>
        /// Generates a random password with the default length.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(int passwordLength)
        {
            return Generate(passwordLength,
                            PasswordCharsAll);
        }

        /// <summary>
        /// Generates a random password.
        /// </summary>
        /// <returns>Randomly generated password.</returns>
        public static string Generate(int passwordLength, string passwordChars)
        {
            return GeneratePassword(passwordLength, passwordChars);
        }

        #endregion


        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <returns></returns>
        private static string GeneratePassword(int passwordLength, string passwordCharacters)
        {
            if (passwordLength < 0)
                throw new ArgumentOutOfRangeException(nameof(passwordLength));

            if (string.IsNullOrEmpty(passwordCharacters))
                throw new ArgumentOutOfRangeException(nameof(passwordCharacters));

            var password = new char[passwordLength];

            var random = GetRandom();

            for (int i = 0; i < passwordLength; i++)
                password[i] = passwordCharacters[random.Next(passwordCharacters.Length)];

            return new string(password);
        }


        /// <summary>
        /// Gets a random object with a real random seed
        /// </summary>
        /// <returns></returns>
        private static Random GetRandom()
        {
            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            new RNGCryptoServiceProvider().GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                        randomBytes[1] << 16 |
                        randomBytes[2] << 8 |
                        randomBytes[3];

            // Now, this is real randomization.
            return new Random(seed);
        }


    }

    public static class StringExtensionHelpers
    {
        public static string IndentedSerialize(this object payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.Indented);
        }

        public static string Serialize(this object payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None);
        }
        public static bool IsNumeric(this object value)
        {
            double retNum;

            var isNumeric = double.TryParse(Convert.ToString(value), NumberStyles.Any, NumberFormatInfo.InvariantInfo, out retNum);
            return isNumeric;
        }
        public static string PrefixWithUser(this string text, string userName)
        {
            return $"{text} => userName: {userName}";
        }

        public static Guid ToGuid(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Guid.Empty;
            return Guid.Parse(value);
        }
        public static bool ToStartsWith(this string val, string query)
        {
            if (val == null) return false;

            query = query == null ? "" : query;

            return val.ToLower().StartsWith(query.ToLower());
        }
        public static string GetLast(this string source, int tailLength)
        {
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }
    }
}
