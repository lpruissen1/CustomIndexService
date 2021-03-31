using StockScreener.Database.Model.Price;
using System;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface IPriceDataRepository
    {
        double GetMostRecentPriceEntry<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
        double GetPriceData<TPriceEntry>(string ticker, TimeSpan timeSpan) where TPriceEntry : PriceData;
        List<Candle> GetPriceData<TPriceEntry>(string ticker) where TPriceEntry : PriceData;
    }
}