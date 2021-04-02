using Database.Core;
using StockScreener.Database.Model.Price;
using System;

namespace StockScreener.Database.Repos
{
    public interface IPriceDataRepository : IBaseRepository<PriceData>
    {
        void Create(HourPriceData obj);
        void Create(DayPriceData obj);
        void Update(HourPriceData obj);
        void Update(DayPriceData obj);
        double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
        double GetPriceData<TPriceEntry>(string ticker, TimeSpan timeSpan) where TPriceEntry : PriceData;
    }
}