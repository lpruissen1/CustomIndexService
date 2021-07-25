namespace StockAggregation.Core
{
	public interface IStockAggregationService
    {
        void UpdateCompanyInfoForMarket(string market);

        void UpdateStockFinancialsForMarket(string market);

        void UpdateDailyPriceDataForMarket(string market);

        void UpdateHourlyPriceDataForMarket(string market);
    }
}
