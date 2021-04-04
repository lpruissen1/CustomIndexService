using StockScreener.Database.Model.Price;

namespace StockScreener.Service.IntegrationTests.StockDataHelpers
{
    public static class PriceDataCreator
    {
        public static DayPriceData GetDailyPriceData(string ticker)
        {
            return new DayPriceData { Ticker = ticker };
        }

        public static DayPriceData AddClosePrice(this DayPriceData dayPriceData, double closePrice, double timestamp = 0)
        {
            dayPriceData.Candle.Add(new Candle { closePrice = closePrice, timestamp = timestamp });
            return dayPriceData;
        }
    }
}
