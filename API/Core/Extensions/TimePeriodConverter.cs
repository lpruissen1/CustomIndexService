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
	}
}
