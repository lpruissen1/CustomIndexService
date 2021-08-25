using Database.Core;
using System;
using System.Collections.Generic;

namespace StockScreener.Database.Model
{
	public class YearDividendData : StockDbEntity
	{
		public DateTime Year { get; set; }
		public List<DividendEntry> Quarters { get; set; } = new List<DividendEntry>();
	}

	public class YearStockFinancials : StockDbEntity
	{
		public DateTime Year { get; set; }
		public List<StockFinancial> Quarters { get; set; } = new List<StockFinancial>();
	}

	public class StockFinancial : Entry
	{
		public double? MarketCap { get; set; }
		public double? SalesPerShare { get; set; }
		public double? BookValuePerShare { get; set; }
		public double? CurrentRatio { get; set; }
		public double? DebtToEquityRatio { get; set; }
		public double? EnterpriseValue { get; set; }
		public double? GrossMargin { get; set; }
		public double? ProfitMargin { get; set; }
		public double? WorkingCapital { get; set; }
	}

	public StockFinancials()
	{
		MarketCap = new List<MarketCap>();
		SalesPerShare = new List<SalesPerShare>();
		BookValuePerShare = new List<BookValuePerShare>();
		CurrentRatio = new List<CurrentRatio>();
		DebtToEquityRatio = new List<DebtToEquityRatio>();
		EnterpriseValue = new List<EnterpriseValue>();
		GrossMargin = new List<GrossMargin>();
		ProfitMargin = new List<ProfitMargin>();
		WorkingCapital = new List<WorkingCapital>();
	}
}
