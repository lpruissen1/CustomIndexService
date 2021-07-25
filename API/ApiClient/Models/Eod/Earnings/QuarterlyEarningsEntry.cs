using System;

namespace ApiClient.Models.Eod.Earnings
{
	public class QuarterlyEarningsEntry
	{
		public DateTime reportDate { get; set; }
		public DateTime date { get; set; }
		public double? epsActual { get; set; }
		public double? epsEstimate { get; set; }
		public double? epsDifference { get; set; }
		public double? surprisePercent { get; set; }
	}
}
