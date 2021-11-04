using System;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static double ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
		}
        public static bool SameDay(this DateTime time, DateTime comparison)
        {
			return time.Year == comparison.Year && time.Month == comparison.Month && time.Day == comparison.Day;
		}

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}

		public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}
	}
}
