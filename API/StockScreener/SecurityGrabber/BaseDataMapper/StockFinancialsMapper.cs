using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.SecurityGrabber.BaseDataMapper
{
    public class StockFinancialsMapper : SecurityMapper<StockFinancials>
    {
        public StockFinancialsMapper()
        {
            blah = new Dictionary<BaseDatapoint, Action<StockFinancials>>()
            {
                {BaseDatapoint.Revenue, AddRevenue },
                {BaseDatapoint.MarketCap, AddMarketCap }
            };
        }

        public virtual void AddRevenue(StockFinancials stockFinancials)
        {
            var present = stockFinancials.Revenues.Last();

            security.QuarterlyRevenue = stockFinancials.Revenues.Select(stock => new RevenueEntry() { Revenue = stock.revenues, Timestamp = stock.timestamp}).ToList();
        }

        public virtual void AddMarketCap(StockFinancials stockFinancials)
        {
            security.MarketCap = stockFinancials.MarketCap.Last().marketCap;
        }
    }

}
