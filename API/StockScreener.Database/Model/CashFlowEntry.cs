using Database.Core;
using System;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class YearCashFlowData : StockDbEntity
	{
		public DateTime Year { get; set; }
		public List<CashFlowEntry> Quarters { get; set; } = new List<CashFlowEntry>();
	}
	public class CashFlowEntry : Entry
	{
		public double? FreeCashFlow { get; set; }
		public double? DividendsPerShare { get; set; }
		public double? PayoutRatio { get; set; }
	}

	public class YearDividendData : StockDbEntity
	{
		public DateTime Year { get; set; }
		public List<DividendEntry> Quarters { get; set; } = new List<DividendEntry>();
	}
	public class DividendEntry : Entry
	{
		public double? DividendsPerShare { get; set; }
		public double? PayoutRatio { get; set; }
	}
}
