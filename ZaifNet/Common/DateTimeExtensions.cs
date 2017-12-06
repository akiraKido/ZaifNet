using System;

namespace ZaifNet.Common
{
    internal static class DateTimeExtensions
    {
        internal static DateTime ToDateTime(this long unixtime)
        {
            return DateTimeOffset.FromUnixTimeSeconds(unixtime).LocalDateTime;
        }
        
        internal static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var utcDateTime = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Utc);
//            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime);
            var dateTimeOffset = new DateTimeOffset(utcDateTime.Ticks, new TimeSpan());
            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}