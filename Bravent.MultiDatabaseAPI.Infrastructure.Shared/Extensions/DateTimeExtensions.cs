
namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Int64? ToUnixTimestamp(this DateTime? date)
        {
            if (date.HasValue) return date.Value.ToUnixTimestamp();
            else return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Int64 ToUnixTimestamp(this DateTime date)
        {
            try
            {
                return Convert.ToInt64(GetTotalSeconds(date));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static double GetTotalSeconds(DateTime date)
        {
            return (date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
        }
    }
}
