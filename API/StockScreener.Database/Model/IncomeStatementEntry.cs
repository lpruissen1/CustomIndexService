namespace StockScreener.Database.Model
{
	public class IncomeStatementEntry : Entry
	{
		public double? Ebitda { get; set; }
		public double? NetIncome { get; set; }
		public double? RotalRevenue { get; set; }
	}
}
