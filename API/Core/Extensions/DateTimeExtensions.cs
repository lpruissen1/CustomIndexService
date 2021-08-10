using System;

namespace Core
{
	public static class DateTimeExtensions
    {
        public static double ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
        public static DateTime FromTimePeriod(this DateTime time, TimePeriod timePeriod)
        {
            return time.AddMonths(GetMonthsPfromTimePeriod(timePeriod) * -1);
        }

        public static bool SameQuarter(this DateTime time, DateTime comparison)
        {
            return time.Year == comparison.Year && GetQuarter(time.Month) == GetQuarter(comparison.Month);
		}

		private static int GetMonthsPfromTimePeriod(TimePeriod timespan)
		{
			switch (timespan)
			{
				case TimePeriod.Month:
					return 1;
				case TimePeriod.Quarter:
					return 3;
				case TimePeriod.HalfYear:
					return 6;
				case TimePeriod.Year:
					return 12;
				case TimePeriod.ThreeYear:
					return 36;
				case TimePeriod.FiveYear:
					return 60;
				default:
					return 0;
			}
		}

		private static int GetQuarter(int month)
		{
			if (month >= 4 && month <= 6)
				return 1;
			else if (month >= 7 && month <= 9)
				return 2;
			else if (month >= 10 && month <= 12)
				return 3;
			else
				return 4;
		}
    }
}
