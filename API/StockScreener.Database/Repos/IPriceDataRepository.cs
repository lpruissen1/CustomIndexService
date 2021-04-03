using Core;
using Database.Core;
using StockScreener.Database.Model.Price;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IPriceDataRepository : IBaseRepository<PriceData>
    {
        void Create(HourPriceData obj);
        void Create(DayPriceData obj);
        void Update(HourPriceData obj);
        void Update(DayPriceData obj);
        double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
        double GetPriceData<TPriceEntry>(string ticker, TimePeriod timeSpan) where TPriceEntry : PriceData;
        List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
    }
}