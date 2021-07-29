using System;

namespace ApiClient.Models.Eod
{
	public class QuarterlyIncomeStatementEntry
	{
		public DateTime? filing_date { get; set; }
		public double? ebitda { get; set; }
		public double? netIncome { get; set; }
		public double? totalRevenue { get; set; }
	}
}
