using Qalam.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Qalam.Common.Helper
{
    public static class HelperFunctions
    {
        private static readonly Random random = new Random();

        public static bool IsStrongPassword(string password)
        {
            bool result = true;

            result &= password.Length >= 8;
            result &= password.HasDigit();
            result &= password.HasLower();
            result &= password.HasUpper();
            result &= password.HasSpecialChar();

            return result;
        }

        public static string RandomString(int length, string chars = null)
        {
            chars ??= "";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)])
              .ToArray());
        }

        public static string RandomStringFromKey(int length, string key)
        {
            var take = Math.Min(key.Length, length / 2);
            StringBuilder randomBuilder = new StringBuilder(new string(key.OrderBy(k => random.Next()).Take(take).ToArray()));
            randomBuilder.Append(RandomString(length - take));
            return new string(randomBuilder.ToString().OrderBy(k => random.Next()).ToArray());
        }

        public static int IntOrDefault(string genericString, int defaultValue = 0)
        {
            try
            {
                return int.Parse(genericString);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}
