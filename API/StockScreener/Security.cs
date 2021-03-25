using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public class Security
    {
        public string Ticker;
        public string Sector;
        public string Industry;
        public double MarketCap;
        public Dictionary<TimeSpan, double> RevenueGrowth;
        public Dictionary<TimeSpan, double> EarningsGrowth;
        public double PriceToEarningsRatioTTM;
        public Dictionary<TimeSpan, double>  TrailingPerformance;

        public void Map(Security security)
        {
            Sector = security.Sector != null ? security.Sector : Sector;
            Industry = security.Industry != null ? security.Industry : Industry;
            MarketCap = security.MarketCap != 0 ? security.MarketCap : MarketCap;
            RevenueGrowth = security.RevenueGrowth != null ? security.RevenueGrowth : RevenueGrowth;
        }
    }

}
