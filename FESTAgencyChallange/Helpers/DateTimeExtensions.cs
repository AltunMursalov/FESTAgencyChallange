using System;

namespace FESTAgencyChallange.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime GetDateTimeFromUnixTimeStamp(double unixTimeStamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp);
            return dt;
        }
    }
}