using Core;
using Database.Core;
using StockScreener.Database.Model.Price;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IPriceDataRepository : IBaseRepository<PriceData>
    {
        void Update(HourPriceData obj);
        void Update(DayPriceData obj);
        double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
        IEnumerable<TPriceEntry> GetClosePriceOverTimePeriod<TPriceEntry>(IEnumerable<string> tickers, TimePeriod timeSpan) where TPriceEntry : PriceData;
        List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
    }
}
