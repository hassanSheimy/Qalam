using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Qalam.Common.Extensions
{
    public static class StringExtensions
    {
        // This method Check if the string contains any digit.
        public static bool HasDigit(this string GenericString)
        {
            return GenericString.Any(ch => Char.IsDigit(ch));
        }

        // This method Check if the string contains any lower case.
        public static bool HasLower(this string GenericString)
        {
            return GenericString.Any(ch => Char.IsLower(ch));
        }

        // This method Check if the string contains any upper case.
        public static bool HasUpper(this string GenericString)
        {
            return GenericString.Any(ch => Char.IsUpper(ch));
        }

        // This method Check if the string contains any upper case.
        public static bool HasSpecialChar(this string GenericString)
        {
            return GenericString.Any(ch => !Char.IsLetterOrDigit(ch));
        }

        public static T ToEnum<T>(this string GenericString)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute && attribute.Description.ToLower() == GenericString.ToLower())
                    return (T)field.GetValue(null);

                if (field.Name.ToLower() == GenericString.ToLower())
                    return (T)field.GetValue(null);
            }
            return default;
        }

        public static string RemoveAllSpaces(this string GenericString)
        {
            string result = "";
            foreach(var ch in GenericString)
            {
                if(ch != ' ')
                {
                    result += ch;
                }
            }
            return result;
        }
    }
}
