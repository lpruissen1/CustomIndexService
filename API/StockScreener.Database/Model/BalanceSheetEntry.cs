namespace StockScreener.Database.Model
{
	public class BalanceSheetEntry : Entry
	{
		public double? TotalAssets { get; set; }
		public double? TotalLiababilites { get; set; }
		public double? CommonStockTotalEquity { get; set; }
		public double? Cash { get; set; }
		public double? NetWorkingCapital { get; set; }
	}
}
