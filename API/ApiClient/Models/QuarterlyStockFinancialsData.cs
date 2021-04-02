using System;

namespace ApiClient.Models
{
	public class QuarterlyStockFinancialsData
    {
		public string Ticker { get; set; }
		public double MarketCapitalization { get; set; }
		public double DividendsPerBasicCommonShare { get; set; }
		public double EarningsPerBasicShare { get; set; }
		public double SalesPerShare { get; set; }
		public double BookValuePerShare { get; set; }
		public double PayoutRatio { get; set; }
		public double CurrentRatio { get; set; }
		public double DebtToEquityRatio { get; set; }
		public double EnterpriseValue { get; set; }
		public double EarningsBeforeInterestTaxesDepreciationAmortization { get; set; }
		public double FreeCashFlow { get; set; }
		public double GrossMargin { get; set; }
		public double ProfitMargin { get; set; }
		public double Revenues { get; set; }
		public double WorkingCapital { get; set; }
		public DateTime ReportPeriod { get; set; }
	}
}
