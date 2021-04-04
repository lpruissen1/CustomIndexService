using StockScreener.Database.Model.StockIndex;

namespace StockScreener.Service.IntegrationTests.StockDataHelpers
{
    public static class StockIndexCreator
    {
        public static StockIndex GetStockIndex(string name)
        {
            return new StockIndex { Name = name };
        }

        public static StockIndex AddTicker(this StockIndex stockIndex, string ticker)
        {
            stockIndex.Tickers.Add(ticker);
            return stockIndex;
        }
    }
}
