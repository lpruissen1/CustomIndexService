using Core;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Calculators
{
    public class DerivedDatapointCalculator : IDerivedDatapointCalculator
    {
        private readonly double yearUnixTime = 31_557_600;
        private readonly double weekErrorFactor = 604_800;

        public SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities, IEnumerable<DerivedDatapointConstructionData> derivedDatapoints)
        {
            var derivedSecurities = new SecuritiesList<DerivedSecurity>();

            foreach (var security in securities) {
                derivedSecurities.Add(new DerivedSecurity()
                {
                    Ticker = security.Ticker,
                    Sector = security.Sector,
                    Industry = security.Industry,
                    RevenueGrowthAnnualized = DeriveRevenueGrowthAnnualized(derivedDatapoints.Where(x => x.datapoint == DerivedDatapoint.RevenueGrowthAnnualized), security),
                    MarketCap = security.MarketCap,
                    PriceToEarningsRatioTTM = DerivePriceToEarningsTTM(derivedDatapoints, security),
                    PayoutRatio = security.PayoutRatio,
                    ProfitMargin = security.ProfitMargin,
                    GrossMargin = security.GrossMargin,
                    WorkingCapital = security.WorkingCapital,
                    DebtToEquityRatio = security.DebtToEquityRatio,
                    FreeCashFlow = security.FreeCashFlow,
                    CurrentRatio = security.CurrentRatio,
                    PriceToSalesRatioTTM = DerivePriceToSalesTTM(derivedDatapoints, security),
                    PriceToBookValue = DerivePriceToBook(derivedDatapoints, security),
                    DividendYield = DeriveDividendYield(derivedDatapoints, security),
                    EPSGrowthAnnualized = DeriveAnnualizedEPSGrowth(derivedDatapoints, security),
                    TrailingPerformance = DeriveTrailingPerformance(derivedDatapoints.Where(x => x.datapoint == DerivedDatapoint.TrailingPerformance), security),
                    RevenueGrowthRaw = DeriveRevenueGrowthRaw(derivedDatapoints.Where(x => x.datapoint == DerivedDatapoint.RevenueGrowthRaw), security)
                });
            }
            
            return derivedSecurities;
        }

        private Dictionary<TimePeriod, double> DeriveRevenueGrowthAnnualized(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any())
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach(var revenueGrowthConstructionData in constructionData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Revenue, past.Revenue, GetUnixFromTimeSpan(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveRevenueGrowthRaw(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any())
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var revenueGrowthConstructionData in constructionData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateGrowthRate(present.Revenue, past.Revenue));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveAnnualizedEPSGrowth(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.EPSGrowthAnnualized))
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var epsGrowthConstructionData in constructionData)
            {
                var span = epsGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyEarnings, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Earnings, past.Earnings, GetUnixFromTimeSpan(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveTrailingPerformance(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any())
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var trailingPerformance in constructionData)
            {
                var span = trailingPerformance.Time;

                var (present, past) = GetEndpointDataForPrice(security.DailyPrice, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Price, past.Price, GetUnixFromTimeSpan(span)));
            }

            return dic;
        }

        private double DerivePriceToEarningsTTM(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.PriceToEarningsRatioTTM))
                return 0;

            var earningsEntries = GetAllEntriesForTimeSpan(security.QuarterlyEarnings, TimePeriod.Year);

            var yearlyEarnings = earningsEntries.Sum(x => x.Earnings);

            return security.DailyPrice.Last().Price / yearlyEarnings;

        }

        private double DeriveDividendYield(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.DividendYield))
                return 0;

            var dividendEntries = GetAllEntriesForTimeSpan(security.QuarterlyDividendsPerShare, TimePeriod.Year);

            var yearlyDividends = dividendEntries.Sum(x => x.QuarterlyDividends);

            return (yearlyDividends / security.DailyPrice.Last().Price) * 100 ;

        }

        private double DerivePriceToBook(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.PriceToBookValue))
                return 0;

            return security.DailyPrice.Last().Price / security.BookValuePerShare;

        }

        private double DerivePriceToSalesTTM(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
            if (!constructionData.Any(x => x.datapoint == DerivedDatapoint.PriceToSalesRatioTTM))
                return 0;

            var salesPerShareEntries = GetAllEntriesForTimeSpan(security.QuarterlySalesPerShare, TimePeriod.Year);

            var yearlySalesPerShare = salesPerShareEntries.Sum(x => x.SalesPerShare);

            return security.DailyPrice.Last().Price / yearlySalesPerShare;

        }

        private (TEntry present, TEntry past) GetEndpointDataForTimeRange<TEntry>(List<TEntry> timeEntries, TimePeriod range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimeSpan(range);
            var past = timeEntries.First(x => x.Timestamp > (present.Timestamp - timeRangeInUnix - weekErrorFactor) && x.Timestamp < (present.Timestamp - timeRangeInUnix + weekErrorFactor));

            return (present, past);
        }

        private (PriceEntry present, PriceEntry past) GetEndpointDataForPrice(List<PriceEntry> timeEntries, TimePeriod range)
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimeSpan(range);
            var past = timeEntries.Last(priceEntry => priceEntry.Timestamp <= present.Timestamp - timeRangeInUnix);

            return (present, past);
        }

        private IEnumerable<TEntry> GetAllEntriesForTimeSpan<TEntry>(List<TEntry> timeEntries, TimePeriod range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last().Timestamp;
            var timeRangeInUnix = GetUnixFromTimeSpan(range);

            return timeEntries.Where(x => x.Timestamp > (present - yearUnixTime - weekErrorFactor));
        }

        private double GetUnixFromTimeSpan(TimePeriod timespan)
        {
            switch (timespan)
            {
                case TimePeriod.Quarter:
                    return yearUnixTime / 4;
                case TimePeriod.HalfYear:
                    return yearUnixTime / 2;
                case TimePeriod.Year:
                    return yearUnixTime;
                case TimePeriod.ThreeYear:
                    return yearUnixTime * 3;
                case TimePeriod.FiveYear:
                    return yearUnixTime * 5;
                default:
                    return 0;
            }
        }
    }

}
