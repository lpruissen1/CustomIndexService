namespace StockScreener.Database.Model
{
	public class BalanceSheetEntry : Entry
	{
		public double? TotalAssets { get; set; }
		public double? TotalLiabilites { get; set; }
		public double? BookValuePerShare { get; set; }
		public double? CommonStockTotalEquity { get; set; }
		public double? Cash { get; set; }
		public double? NetWorkingCapital { get; set; }
		public double? MarketCap { get; set; }
		public double? CurrentRatio { get; set; }
		public double? DebtToEquityRatio { get; set; }
	}
}
