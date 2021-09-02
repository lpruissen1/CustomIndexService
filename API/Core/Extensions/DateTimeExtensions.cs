using System;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static double ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
		}

		public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dateTime;
		}
	}
}
