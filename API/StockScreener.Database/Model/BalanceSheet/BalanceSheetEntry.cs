namespace StockScreener.Database.Model.BalanceSheet
{
	public class BalanceSheetEntry : Entry
	{
		public decimal Cash { get; set; }
		public decimal TotalAssets { get; set; }
		public decimal TotalLiabilities { get; set; }
		public decimal CommonStockEquityOutstanding{ get; set; }
	}
}
