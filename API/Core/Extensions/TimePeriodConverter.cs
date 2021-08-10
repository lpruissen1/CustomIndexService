namespace Core.Extensions
{
	public static class TimePeriodConverter
	{
		private static double secondsInAYear = 31_557_600;

		public static double GetSecondsFromTimePeriod(TimePeriod timespan)
		{
			switch (timespan)
			{
				case TimePeriod.Quarter:
					return secondsInAYear / 4;
				case TimePeriod.HalfYear:
					return secondsInAYear / 2;
				case TimePeriod.Year:
					return secondsInAYear;
				case TimePeriod.ThreeYear:
					return secondsInAYear * 3;
				case TimePeriod.FiveYear:
					return secondsInAYear * 5;
				default:
					return 0;
			}
		}

		public static int GetMonthsFromTimePeriod(this TimePeriod time)
		{
			switch (time)
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
	}
}
