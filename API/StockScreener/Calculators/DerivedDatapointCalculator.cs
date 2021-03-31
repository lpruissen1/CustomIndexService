﻿using StockScreener.Core;
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
            var derivedSecurities = new SecuritiesList<DerivedSecurity>();

            foreach (var security in securities) {
                derivedSecurities.Add(new DerivedSecurity()
                {
                    Ticker = security.Ticker,
                    Sector = security.Sector,
                    Industry = security.Industry,
                    RevenueGrowth = DeriveRevenueGrowth(derivedDatapoints.Where(x => x.datapoint == DerivedDatapoint.RevenueGrowth), security),
                    MarketCap = security.MarketCap,
                    PriceToEarningsRatioTTM = DerivePriceToEarningsTTM(derivedDatapoints, security),
                    PayoutRatio = security.PayoutRatio,
                    ProfitMargin = security.ProfitMargin,
                    GrossMargin = security.GrossMargin
                });
            }
            
            return derivedSecurities;
        }

        private Dictionary<TimeSpan, double> DeriveRevenueGrowth(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any())
                return null;

            var dic = new Dictionary<TimeSpan, double>();

            foreach(var revenueGrowthConstructionData in constructionData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Revenue, past.Revenue, GetUnixFromTimeSpan(span)));
            }

            return dic;
        }

        private double DerivePriceToEarningsTTM(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.PriceToEarningsRatioTTM))
                return 0;

            var earningsEntries = GetAllEntriesForTimeSpan(security.QuarterlyEarnings, TimeSpan.OneYear);

            var yearlyEarnings = earningsEntries.Sum(x => x.Earnings);

            return security.DailyPrice.Last().Price / yearlyEarnings;

        }

        private (TEntry present, TEntry past) GetEndpointDataForTimeRange<TEntry>(List<TEntry> timeEntries, TimeSpan range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimeSpan(range);
            var past = timeEntries.First(x => x.Timestamp > (present.Timestamp - timeRangeInUnix - errorFactor) && x.Timestamp < (present.Timestamp - timeRangeInUnix + errorFactor));

            return (present, past);
        }

        private IEnumerable<TEntry> GetAllEntriesForTimeSpan<TEntry>(List<TEntry> timeEntries, TimeSpan range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last().Timestamp;
            var timeRangeInUnix = GetUnixFromTimeSpan(range);

            return timeEntries.Where(x => x.Timestamp > (present - yearUnixTime - errorFactor));
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
