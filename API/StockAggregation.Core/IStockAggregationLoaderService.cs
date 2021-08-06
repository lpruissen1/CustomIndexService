namespace StockAggregation.Core
{
	public interface IStockAggregationLoaderService
    {
		void LoadStockFundementalData(string index);
        void LoadDailyPriceDataForIndex(string index);
    }
}
