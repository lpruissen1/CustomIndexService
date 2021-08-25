namespace StockScreener.Database.Model
{
	public class DividendEntry : Entry
	{
		public double? DividendsPerShare { get; set; }
		public double? PayoutRatio { get; set; }
	}
}
