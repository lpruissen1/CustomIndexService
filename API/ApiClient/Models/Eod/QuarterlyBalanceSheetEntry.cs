using System;

namespace ApiClient.Models.Eod
{
	public class QuarterlyBalanceSheetEntry
	{
		public DateTime? filing_date { get; set; }
		public double? totalAssets { get; set; }
		public double? totalLiab { get; set; }
		public double? commonStockTotalEquity { get; set; }
		public double? cash { get; set; }
		public double? netWorkingCapital { get; set; }
	}
}
