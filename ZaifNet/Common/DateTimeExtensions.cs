using System;

namespace ZaifNet.Common
{
    internal static class DateTimeExtensions
    {
        internal static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            var utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTime);
            var dateTimeOffset = new DateTimeOffset(utcDateTime.Ticks, new TimeSpan());
            return dateTimeOffset.ToUnixTimeSeconds();
        }
    }
}