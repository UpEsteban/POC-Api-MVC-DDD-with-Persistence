using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class Int64Extensions
    {
        private static DateTime GetUtcDateTime(long input)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(input);
        }

        private static DateTime GetConvertedDate(string timeZone, DateTime date)
        {
            return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Int64 input, String timeZone = "UTC")
        {
            DateTime date = GetUtcDateTime(input);

            if (!String.IsNullOrEmpty(timeZone) && timeZone != "UTC") date = GetConvertedDate(timeZone, date);

            return date;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(this Int64? input, String timeZone = "UTC")
        {
            if (input.HasValue) return input.Value.ToDateTime(timeZone);
            else return null;
        }
    }
}
