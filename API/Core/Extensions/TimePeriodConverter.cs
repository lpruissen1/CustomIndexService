namespace Core.Extensions
{
	public static class TimePeriodConverter
	{
		private static double yearUnixTime = 31_557_600;

		public static double GetUnixFromTimePeriod(TimePeriod timespan)
		{
			switch (timespan)
			{
				case TimePeriod.Quarter:
					return yearUnixTime / 4;
				case TimePeriod.HalfYear:
					return yearUnixTime / 2;
				case TimePeriod.Year:
					return yearUnixTime;
				case TimePeriod.ThreeYear:
					return yearUnixTime * 3;
				case TimePeriod.FiveYear:
					return yearUnixTime * 5;
				default:
					return 0;
			}
		}
	}
}
