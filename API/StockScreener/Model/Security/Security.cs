using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public abstract class Security
    {
        public string Ticker;
        public string Sector;
        public string Industry;
        public double MarketCap;
    }

    public class BaseSecurity : Security
    {
        public List<EarningsEntry> QuarterlyEarnings;
        public List<RevenueEntry> QuarterlyRevenue;
        public List<PriceEntry> DailyPrice;

        public void Map(BaseSecurity security)
        {
            Sector = security.Sector != null ? security.Sector : Sector;
            Industry = security.Industry != null ? security.Industry : Industry;
            MarketCap = security.MarketCap != 0 ? security.MarketCap : MarketCap;
            QuarterlyEarnings = security.QuarterlyEarnings != null ? security.QuarterlyEarnings : QuarterlyEarnings;
            QuarterlyRevenue = security.QuarterlyRevenue != null ? security.QuarterlyRevenue : QuarterlyRevenue;
            MarketCap = security.MarketCap != 0 ? security.MarketCap : MarketCap;
        }
    }

    public class DerivedSecurity : Security
    {
        public Dictionary<TimeSpan, double> RevenueGrowth;
        public Dictionary<TimeSpan, double> EarningsGrowth;
        public double PriceToEarningsRatioTTM;
        public Dictionary<TimeSpan, double> TrailingPerformance;
    }

    public class TimeEntry
    {
        public double Timestamp;
    }

    public class RevenueEntry : TimeEntry
    {
        public double Revenue;
    }

    public class EarningsEntry : TimeEntry
    {
        public double Earnings;
    }

    public class PriceEntry : TimeEntry
    {
        public double Price;
    }

}
