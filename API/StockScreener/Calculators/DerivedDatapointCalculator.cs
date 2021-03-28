using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Calculators
{
    public class DerivedDatapointCalculator : IDerivedDatapointCalculator
    {
        private readonly double yearUnixTime = 31_557_600;
        private readonly double errorFactor = 604_800;

        public SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities, IEnumerable<DerivedDatapointConstructionData> derivedDatapoints)
        {
            var blah = new SecuritiesList<DerivedSecurity>();

            foreach (var security in securities) {
                blah.Add(new DerivedSecurity() { Ticker = security.Ticker, RevenueGrowth = DeriveRevenueGrowth(derivedDatapoints.Where(x => x.datapoint == DerivedDatapoint.RevenueGrowth), security) });
            }
            
            return blah;
        }

        private void SectorAndIndustry()
        {

        }

        private Dictionary<TimeSpan, double> DeriveRevenueGrowth(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any())
                return null;
            // TODO:
            // use generic annualizer to annualize
            var dic = new Dictionary<TimeSpan, double>();

            foreach(var revenueGrowthConstructionData in constructionData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetGrowthOverTime(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Revenue, past.Revenue, GetUnixFromTimeSpan(span)));
            }

            return dic;
        }

        private (TEntry present, TEntry past) GetGrowthOverTime<TEntry>(List<TEntry> timeEntries, TimeSpan range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimeSpan(range);
            var past = timeEntries.First(x => x.Timestamp > (present.Timestamp - timeRangeInUnix - errorFactor) && x.Timestamp < (present.Timestamp - timeRangeInUnix + errorFactor));

            return (present, past);
        }

        private double GetUnixFromTimeSpan(TimeSpan timespan)
        {
            switch (timespan)
            {
                case TimeSpan.Quarterly:
                    return yearUnixTime / 4;
                case TimeSpan.HalfYear:
                    return yearUnixTime / 2;
                case TimeSpan.OneYear:
                    return yearUnixTime;
                case TimeSpan.ThreeYear:
                    return yearUnixTime * 3;
                case TimeSpan.FiveYear:
                    return yearUnixTime * 5;
                default:
                    return 0;
            }
        }
    }

}
