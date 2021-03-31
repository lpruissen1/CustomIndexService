using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
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
            PayoutRatio = security.PayoutRatio != 0 ? security.PayoutRatio : PayoutRatio;
            ProfitMargin = security.ProfitMargin != 0 ? security.ProfitMargin : ProfitMargin;
            GrossMargin = security.GrossMargin != 0 ? security.GrossMargin : GrossMargin;
        }
    }

}
