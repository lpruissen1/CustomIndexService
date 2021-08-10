using System;

namespace StockScreener.Database.Model
{
	public class EarningsEntry : Entry
	{
		public DateTime ReportDate { get; set; }
		public double EaringsPerShare { get; set; }
		public double EBITA { get; set; }
	}
}
