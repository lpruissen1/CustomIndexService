using Core;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Calculators
{
    public class DerivedDatapointCalculator : IDerivedDatapointCalculator
    {
        private readonly double yearUnixTime = 31_557_600;
        private readonly double weekErrorFactor = 604_800;

		// have a function ampper to reduce work done here
        public SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities, IEnumerable<DerivedDatapointConstructionData> derivedDatapoints)
        {
            var derivedSecurities = new SecuritiesList<DerivedSecurity>();

            foreach (var security in securities) {
                derivedSecurities.Add(new DerivedSecurity()
                {
                    Ticker = security.Ticker,
                    Sector = security.Sector,
                    Industry = security.Industry,
                    RevenueGrowthAnnualized = DeriveRevenueGrowthAnnualized(derivedDatapoints, security),
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
                    TrailingPerformanceAnnualized = DeriveAnnualizedTrailingPerformance(derivedDatapoints, security),
                    RevenueGrowthRaw = DeriveRevenueGrowthRaw(derivedDatapoints, security),
                    EPSGrowthRaw = DeriveRawEPSGrowth(derivedDatapoints, security),
                    DividendGrowthAnnualized = DeriveDividendGrowthAnnualized(derivedDatapoints, security),
                    DividendGrowthRaw = DeriveDividendGrowthRaw(derivedDatapoints, security),
                    TrailingPerformanceRaw = DeriveRawTrailingPerformance(derivedDatapoints, security),
                    CoefficientOfVariation = DeriveCoefficientOfVariation(derivedDatapoints, security)
                });
            }
            
            return derivedSecurities;
        }

        private Dictionary<TimePeriod, double> DeriveRevenueGrowthAnnualized(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.RevenueGrowthAnnualized);

			if (!relevantData.Any())
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach(var revenueGrowthConstructionData in relevantData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Revenue, past.Revenue, GetUnixFromTimePeriod(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveRevenueGrowthRaw(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
        {
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.RevenueGrowthRaw);

			if (!relevantData.Any())
                return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var revenueGrowthConstructionData in relevantData)
            {
                var span = revenueGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, span);
                dic.Add(span, GrowthRateCalculator.CalculateGrowthRate(present.Revenue, past.Revenue));
            }

            return dic;
        }

		private IEnumerable<DerivedDatapointConstructionData> GetReleventDatapoints(IEnumerable<DerivedDatapointConstructionData> datapoints, DerivedDatapoint type)
		{
			return datapoints.Where(x => x.datapoint == type);
		}

        private Dictionary<TimePeriod, double> DeriveAnnualizedEPSGrowth(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.EPSGrowthAnnualized);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var epsGrowthConstructionData in relevantData)
            {
                var span = epsGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyEarnings, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Earnings, past.Earnings, GetUnixFromTimePeriod(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveRawEPSGrowth(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.EPSGrowthRaw);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var epsGrowthConstructionData in relevantData)
            {
                var span = epsGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyEarnings, span);
                dic.Add(span, GrowthRateCalculator.CalculateGrowthRate(present.Earnings, past.Earnings));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveDividendGrowthAnnualized(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.DividendGrowthAnnualized);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var dividendGrowthConstructionData in relevantData)
            {
                var span = dividendGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyDividendsPerShare, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.QuarterlyDividends, past.QuarterlyDividends, GetUnixFromTimePeriod(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveDividendGrowthRaw(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.DividendGrowthRaw);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var dividendGrowthConstructionData in relevantData)
            {
                var span = dividendGrowthConstructionData.Time;

                var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyDividendsPerShare, span);
                dic.Add(span, GrowthRateCalculator.CalculateGrowthRate(present.QuarterlyDividends, past.QuarterlyDividends));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveAnnualizedTrailingPerformance(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.TrailingPerformanceAnnualized);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var trailingPerformance in relevantData)
            {
                var span = trailingPerformance.Time;

                var (present, past) = GetEndpointDataForPrice(security.DailyPrice, span);
                dic.Add(span, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Price, past.Price, GetUnixFromTimePeriod(span)));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveRawTrailingPerformance(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.TrailingPerformanceRaw);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var trailingPerformance in relevantData)
            {
                var span = trailingPerformance.Time;

                var (present, past) = GetEndpointDataForPrice(security.DailyPrice, span);
                dic.Add(span, GrowthRateCalculator.CalculateGrowthRate(present.Price, past.Price));
            }

            return dic;
        }

        private Dictionary<TimePeriod, double> DeriveCoefficientOfVariation(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.CoefficientOfVariation);

			if (!relevantData.Any())
				return null;

            var dic = new Dictionary<TimePeriod, double>();

            foreach (var timePeriod in relevantData)
            {
                var span = timePeriod.Time;

                var priceEntries = security.DailyPrice.Where(x => x.Timestamp > DateTime.UtcNow.ToUnix() - GetUnixFromTimePeriod(timePeriod.Time));

                var standardDeviation = priceEntries.Select(x => x.Price).StandardDeviation();

                var coefficients = standardDeviation / priceEntries.Select(x => x.Price).Average() * 100;

                dic.Add(span, coefficients);
            }

            return dic;
        }

        private double DerivePriceToEarningsTTM(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.PriceToEarningsRatioTTM);

			if (!relevantData.Any())
				return 0;

            var earningsEntries = GetAllEntriesForTimeSpan(security.QuarterlyEarnings, TimePeriod.Year);

            var yearlyEarnings = earningsEntries.Sum(x => x.Earnings);

            return security.DailyPrice.Last().Price / yearlyEarnings;

        }

        private double DeriveDividendYield(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.DividendYield);

			if (!relevantData.Any())
				return 0;

            var dividendEntries = GetAllEntriesForTimeSpan(security.QuarterlyDividendsPerShare, TimePeriod.Year);

            var yearlyDividends = dividendEntries.Sum(x => x.QuarterlyDividends);

            return (yearlyDividends / security.DailyPrice.Last().Price) * 100 ;

        }

        private double DerivePriceToBook(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.PriceToBookValue);

			if (!relevantData.Any())
				return 0;

            return security.DailyPrice.Last().Price / security.BookValuePerShare;
        }

        private double DerivePriceToSalesTTM(IEnumerable<DerivedDatapointConstructionData> constructionData, BaseSecurity security)
		{
			var relevantData = GetReleventDatapoints(constructionData, DerivedDatapoint.PriceToSalesRatioTTM);

			if (!relevantData.Any())
				return 0;

            var salesPerShareEntries = GetAllEntriesForTimeSpan(security.QuarterlySalesPerShare, TimePeriod.Year);

            var yearlySalesPerShare = salesPerShareEntries.Sum(x => x.SalesPerShare);

            return security.DailyPrice.Last().Price / yearlySalesPerShare;

        }

        private (TEntry present, TEntry past) GetEndpointDataForTimeRange<TEntry>(List<TEntry> timeEntries, TimePeriod range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimePeriod(range);
            var past = timeEntries.First(x => x.Timestamp > (present.Timestamp - timeRangeInUnix - weekErrorFactor) && x.Timestamp < (present.Timestamp - timeRangeInUnix + weekErrorFactor));

            return (present, past);
        }

        private (PriceEntry present, PriceEntry past) GetEndpointDataForPrice(List<PriceEntry> timeEntries, TimePeriod range)
        {
            var present = timeEntries.Last();
            var timeRangeInUnix = GetUnixFromTimePeriod(range);
            var past = timeEntries.Last(priceEntry => priceEntry.Timestamp <= present.Timestamp - timeRangeInUnix);

            return (present, past);
        }

        private IEnumerable<TEntry> GetAllEntriesForTimeSpan<TEntry>(List<TEntry> timeEntries, TimePeriod range) where TEntry : TimeEntry
        {
            var present = timeEntries.Last().Timestamp;
            var timeRangeInUnix = GetUnixFromTimePeriod(range);

            return timeEntries.Where(x => x.Timestamp > (present - yearUnixTime - weekErrorFactor));
        }

        private double GetUnixFromTimePeriod(TimePeriod timespan)
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
