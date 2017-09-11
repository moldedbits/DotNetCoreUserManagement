using System;

namespace UserAppService.Utility.Extensions
{
    public static class DateExtensions
    {
        /// <summary>
        /// Get the date of the coming Sunday from this target date
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime GetDateOfNextSun(this DateTime target)
        {
            return target.DayOfWeek == DayOfWeek.Sunday ? target.Date : target.AddDays(7 - ( int )target.DayOfWeek).Date;
        }

        public static DateTime Truncate(this DateTime dateTime, TimeSpan timeSpan)
        {
            if(timeSpan == TimeSpan.Zero) return dateTime;
            return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
        }

        public static class TruncatedDateTime
        {
            public static Func<DateTime> UtcNow = () => DateTime.UtcNow.Truncate(TimeSpan.FromSeconds(1));
        }

        public static double ToUnixTimestamp(this DateTime dateTime)
        {
            var ts = dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            return ( long )Math.Truncate(ts.TotalSeconds);

        }

        public static DateTime ToDateTime(this double unixTimeStamp, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(unixTimeStamp.ToDateTime(), targetTimeZoneInfo);
        }

        public static DateTime ToDateTime(this double unixTimeStamp)
        {
            var origin = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0), DateTimeKind.Utc);

            return origin.AddSeconds(unixTimeStamp);

        }

        public static DateTime ToCentralTime(this DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.FindSystemTimeZoneById(TimeZones.CentralStandardTime));
        }

        public static DateTime ToUTCFromCentralTime(this DateTime centralDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(centralDateTime, TimeZoneInfo.FindSystemTimeZoneById(TimeZones.CentralStandardTime));
        }

        public class TimeZones
        {
            #region Fields
            public const string CentralStandardTime = "Central Standard Time";
            public const string EasternStandardTime = "Eastern Standard Time";
            public const string MountainStandardTime = "Mountain Standard Time";
            public const string PacificStandardTime = "Pacific Standard Time";
            #endregion
        }
    }
}
