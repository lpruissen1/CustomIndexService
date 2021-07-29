namespace StockAggregation.Core
{
	public interface IStockAggregationLoaderService
    {
        void LoadCompanyInfoForExchange(string index);
        void LoadDailyPriceDataForExchange(string index);
        void LoadHourlyPriceDataForExchange(string index);
		void LoadEarningsForExchange(string index);
        void LoadOutstandingSharesForExchange(string index);
        void LoadBalanceSheetForExchange(string index);
        void LoadIncomeStatementForExchange(string index);
        void LoadCashFlowForExchange(string index);
    }
}
