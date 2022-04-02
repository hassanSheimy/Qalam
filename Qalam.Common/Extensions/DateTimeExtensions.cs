using System;
using System.Collections.Generic;
using System.Text;

namespace Qalam.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dateTime.DayOfWeek - startOfWeek)) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DayOfWeek DayOfWeek(this DateTime dateTime, DayOfWeek startOfWeek)
        {
            int restOfDays = 7 - (int)startOfWeek;
            int day = ((int)dateTime.DayOfWeek + restOfDays) % 7;
            return (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), day);
        }

        public static DayOfWeek DayOfWeek(this DayOfWeek day, DayOfWeek startOfWeek)
        {
            int restOfDays = 7 - (int)startOfWeek;
            int currentDay = ((int)day + restOfDays) % 7;
            return (DayOfWeek)Enum.ToObject(typeof(DayOfWeek), currentDay);
        }
    }
}
