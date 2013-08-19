using System;
using System.Collections.Generic;
using System.Text;

namespace libeveapi
{
    class TimeUtilities
    {
        /// <summary>
        /// Converts a CCP API date/time string to a UTC DateTime
        /// </summary>
        /// <param name="timeUTC"></param>
        /// <returns></returns>
        public static DateTime ConvertCCPTimeStringToDateTimeUTC(string timeUTC)
        {
            // timeUTC  = yyyy-mm-dd hh:mm:ss
            if (timeUTC == null || timeUTC == "")
                return DateTime.MinValue;

            DateTime dt = new DateTime(
                            Int32.Parse(timeUTC.Substring(0, 4)),
                            Int32.Parse(timeUTC.Substring(5, 2)),
                            Int32.Parse(timeUTC.Substring(8, 2)),
                            Int32.Parse(timeUTC.Substring(11, 2)),
                            Int32.Parse(timeUTC.Substring(14, 2)),
                            Int32.Parse(timeUTC.Substring(17, 2)),
                            0,
                            DateTimeKind.Utc);
            return dt;
        }

        public  static DateTime GetCacheExpiryUTC(DateTime cacheUntil, DateTime CCPCurrent)
        {
            return DateTime.Now.ToUniversalTime() + (cacheUntil - CCPCurrent);
        }

        public static DateTime ConvertCCPToLocalTime(DateTime ccpDateTime)
        {
            return ccpDateTime.ToLocalTime();
        }
    }
}
