namespace StockScreener.Database.Model
{
	public class CashFlowEntry : Entry
	{
		public double? DividendsPaid { get; set; }
		public double? FreeCashFlow { get; set; }
		public double? DividendsPerShare { get; set; }
		public double? PayoutRatio { get; set; }
	}
}
