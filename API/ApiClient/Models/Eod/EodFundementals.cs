namespace ApiClient.Models.Eod
{
	public class EodFundementals
	{
		public string Ticker { get; set; }
		public EodCompanyInfo General { get; set; }
		public EodFinancials Financials { get; set; }
		public EodOutstandingShares outstandingShares { get; set; }
		public EodEarnings Earnings { get; set; }
	}

	public class EodFinancials
	{
		public EodBalanceSheet Balance_Sheet { get; set; }
		public EodCashFlow Cash_Flow { get; set; }
		public EodIncomeStatement Income_Statement { get; set; }
	}
}
