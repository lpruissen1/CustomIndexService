namespace StockScreener.Database.Model
{
	public class EarningsEntry : Entry
	{
		public double ReportDate { get; set; }
		public double EaringsPerShare { get; set; }
		public double EaringsPerShareEstimate { get; set; }
	}
}
