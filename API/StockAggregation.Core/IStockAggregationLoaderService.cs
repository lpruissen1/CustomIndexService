namespace StockAggregation.Core
{
	public interface IStockAggregationLoaderService
    {
        void LoadCompanyInfoForExchange(string exchange);

        void LoadStockFinancialsForExchange(string exchange);

        void LoadDailyPriceDataForExchange(string exchange);

        void LoadHourlyPriceDataForExchange(string exchange);

        void LoadEarningsForExchange(string exchange);

        void LoadOutstandingSharesForExchange(string exchange);
    }
}
