using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.BLL.Configurations
{
    public sealed class CsvConverter<T> : DefaultTypeConverter
    {
        /// <summary>
        /// Convert from string to generic type t
        /// </summary>
        /// <param name="text"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns> (if text is null or enpty then return defult value of type T, if text faild to convert then return null otherwise return the converted value) </returns>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    return memberMapData.Default;
                }
                return (T)Convert.ChangeType(text, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Convert object To String
        /// </summary>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="memberMapData"></param>
        /// <returns></returns>
        public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            try
            {
                return value.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
