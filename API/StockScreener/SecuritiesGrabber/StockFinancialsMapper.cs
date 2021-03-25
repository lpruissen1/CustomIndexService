using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSpan = StockScreener.Core.TimeSpan;

namespace StockScreener
{
    public class StockFinancialsMapper : SecurityMapper<StockFinancials>
    {
        private readonly double yearUnixTime = 31_557_600;
        private readonly double errorFactor = 604_800;

        public StockFinancialsMapper()
        {
            blah = new Dictionary<Datapoint, Action<StockFinancials>>()
            {
                {Datapoint.Revenue, AddRevenue }
            };
        }

        public virtual void AddRevenue(StockFinancials stockFinancials)
        {
            var present = stockFinancials.Revenues.Last();
            var presentTimeStamp = present.timestamp;
            var quarter = stockFinancials.Revenues.First(x => x.timestamp > (presentTimeStamp - (yearUnixTime / 4) - errorFactor) && x.timestamp < (presentTimeStamp - (yearUnixTime / 4) + errorFactor));

            security.RevenueGrowth = new Dictionary<TimeSpan, double> {
                { TimeSpan.Quarterly, GrowthRateCalculator.CalculateGrowthRate(present.revenues, quarter.revenues)}
                // Add more timespans
            };
        }
    }

}
