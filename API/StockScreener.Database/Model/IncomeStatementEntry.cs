namespace StockScreener.Database.Model
{
	public class IncomeStatementEntry : Entry
	{
		public double? Ebitda { get; set; }
		public double? NetIncome { get; set; }
		public double? TotalRevenue { get; set; }
		public double? SalesPerShare { get; set; }
	}
}
