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
        private readonly double thirtySixHourErrorFactor = 129_600;

		private Dictionary<DerivedDatapoint, Action<DerivedSecurity, TimePeriod, BaseSecurity>> timedRangeFunctionMapper;
		private Dictionary<DerivedDatapoint, Action<DerivedSecurity, BaseSecurity>> ruleFunctionMapper;

		public DerivedDatapointCalculator()
		{
			timedRangeFunctionMapper = new Dictionary<DerivedDatapoint, Action<DerivedSecurity, TimePeriod, BaseSecurity>>()
			{
				{DerivedDatapoint.RevenueGrowthAnnualized, DeriveRevenueGrowthAnnualized },
				{DerivedDatapoint.EPSGrowthAnnualized, DeriveAnnualizedEPSGrowth },
				{DerivedDatapoint.DividendGrowthAnnualized, DeriveDividendGrowthAnnualized },
				{DerivedDatapoint.CoefficientOfVariation, DeriveCoefficientOfVariation },
				{DerivedDatapoint.TrailingPerformanceAnnualized, DeriveAnnualizedTrailingPerformance }
			};

			ruleFunctionMapper = new Dictionary<DerivedDatapoint, Action<DerivedSecurity, BaseSecurity>>()
			{
				{DerivedDatapoint.PriceToEarningsRatioTTM, DerivePriceToEarningsTTM },
				{DerivedDatapoint.DividendYield, DeriveDividendYield },
				{DerivedDatapoint.PriceToSalesRatioTTM, DerivePriceToSalesTTM },
				{DerivedDatapoint.Industry, DeriveIndustry },
				{DerivedDatapoint.Sector, DeriveSector },
				{DerivedDatapoint.MarketCap, DeriveMarketCap },
				{DerivedDatapoint.PayoutRatio, DerivePayoutRatio },
				{DerivedDatapoint.ProfitMargin, DeriveProfitMargin },
				{DerivedDatapoint.GrossMargin, DeriveGrossMargin },
				{DerivedDatapoint.WorkingCapital, DeriveWorkingCapital },
				{DerivedDatapoint.DebtToEquityRatio, DeriveDebtToEquityRatio },
				{DerivedDatapoint.FreeCashFlow, DeriveFreeCashFlow },
				{DerivedDatapoint.CurrentRatio, DeriveCurrentRatio }
			};
		}

		public SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities, IEnumerable<DerivedDatapointConstructionData> derivedDatapoints)
        {
            var derivedSecurities = new SecuritiesList<DerivedSecurity>();

            foreach (var security in securities) {
				var derivedSecurity = new DerivedSecurity();
				derivedSecurity.Ticker = security.Ticker;

				foreach(var derivedDatapoint in derivedDatapoints)
				{
					if(timedRangeFunctionMapper.TryGetValue(derivedDatapoint.datapoint, out var timedRangefunction))
						timedRangefunction.Invoke(derivedSecurity, derivedDatapoint.Time, security);

					if (ruleFunctionMapper.TryGetValue(derivedDatapoint.datapoint, out var basicRuleFunction))
						basicRuleFunction.Invoke(derivedSecurity, security);
					
				}

				derivedSecurities.Add(derivedSecurity);
            }
            
            return derivedSecurities;
        }

        private void DeriveRevenueGrowthAnnualized(DerivedSecurity derivedSecurity, TimePeriod timePeriod, BaseSecurity security)
        {
			if (!security.QuarterlyRevenue.Any())
			{
				derivedSecurity.RevenueGrowthAnnualized.Add(timePeriod, null);
				return;
			}
			
			var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyRevenue, timePeriod, weekErrorFactor);

			if (past is not null)
			{
				derivedSecurity.RevenueGrowthAnnualized.Add(timePeriod, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Revenue, past.Revenue, GetUnixFromTimePeriod(timePeriod)));
				return;
			}
			
			derivedSecurity.RevenueGrowthAnnualized.Add(timePeriod, null);
		}

        private void DeriveAnnualizedEPSGrowth(DerivedSecurity derivedSecurity, TimePeriod timePeriod, BaseSecurity security)
		{
			if (!security.QuarterlyEarnings.Any())
			{
				derivedSecurity.EPSGrowthAnnualized.Add(timePeriod, null);
				return;
			}

			var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyEarnings, timePeriod, weekErrorFactor);

			if (past is not null)
			{
				derivedSecurity.EPSGrowthAnnualized.Add(timePeriod, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Earnings, past.Earnings, GetUnixFromTimePeriod(timePeriod)));
				return;
			}

			derivedSecurity.EPSGrowthAnnualized.Add(timePeriod, null);
        }

        private void DeriveDividendGrowthAnnualized(DerivedSecurity derivedSecurity, TimePeriod timePeriod, BaseSecurity security)
		{
			if (!security.QuarterlyDividendsPerShare.Any())
			{
				derivedSecurity.DividendGrowthAnnualized.Add(timePeriod, null);
				return;
			}

			var (present, past) = GetEndpointDataForTimeRange(security.QuarterlyDividendsPerShare, timePeriod, weekErrorFactor);

			if (past is not null)
			{
				derivedSecurity.DividendGrowthAnnualized.Add(timePeriod, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.QuarterlyDividends, past.QuarterlyDividends, GetUnixFromTimePeriod(timePeriod)));
				return;
			}

			derivedSecurity.DividendGrowthAnnualized.Add(timePeriod, null);
        }

        private void DeriveAnnualizedTrailingPerformance(DerivedSecurity derivedSecurity, TimePeriod timePeriod, BaseSecurity security)
		{
			if (!security.DailyPrice.Any())
			{
				derivedSecurity.TrailingPerformanceAnnualized.Add(timePeriod, null);
				return;
			}

			var (present, past) = GetEndpointDataForTimeRange(security.DailyPrice, timePeriod, thirtySixHourErrorFactor);

			if (past is not null)
			{
				derivedSecurity.TrailingPerformanceAnnualized.Add(timePeriod, GrowthRateCalculator.CalculateAnnualizedGrowthRate(present.Price, past.Price, GetUnixFromTimePeriod(timePeriod)));
				return;
			}

			derivedSecurity.TrailingPerformanceAnnualized.Add(timePeriod, null);
        }

		private void DeriveCoefficientOfVariation(DerivedSecurity derivedSecurity, TimePeriod timePeriod, BaseSecurity security)
		{
			var (_, past) = GetEndpointDataForTimeRange(security.DailyPrice, timePeriod, thirtySixHourErrorFactor);

			if (past is not null)
			{
				var priceEntries = security.DailyPrice.Where(x => x.Timestamp > past.Timestamp);
				var standardDeviation = priceEntries.Select(x => x.Price).StandardDeviation();

				var coefficients = standardDeviation / priceEntries.Select(x => x.Price).Average() * 100;

				derivedSecurity.CoefficientOfVariation.Add(timePeriod, coefficients);
				return;
			}

			derivedSecurity.CoefficientOfVariation.Add(timePeriod, null);
		}

		private void DerivePriceToEarningsTTM(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			if (!security.QuarterlyEarnings.Any() || !security.DailyPrice.Any())
			{
				derivedSecurity.PriceToEarningsRatioTTM = null;
				return;
			}

            var earningsEntries = GetAllEntriesForTimeSpan(security.QuarterlyEarnings, TimePeriod.Year);
			if (earningsEntries.Count() < 4)
			{
				derivedSecurity.PriceToEarningsRatioTTM = null;
				return;
			}

            var yearlyEarnings = earningsEntries.Sum(x => x.Earnings);

			derivedSecurity.PriceToEarningsRatioTTM = security.DailyPrice.Last().Price / yearlyEarnings;
        }

        private void DeriveDividendYield(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			if (!security.QuarterlyDividendsPerShare.Any() || !security.DailyPrice.Any())
			{
				derivedSecurity.DividendYield = null;
				return;
			}

			var dividendEntries = GetAllEntriesForTimeSpan(security.QuarterlyDividendsPerShare, TimePeriod.Year);

			if (dividendEntries.Count() < 4)
			{
				derivedSecurity.DividendYield = null;
				return;
			}

			var yearlyDividend = dividendEntries.Sum(x => x.QuarterlyDividends);

			derivedSecurity.DividendYield = (yearlyDividend / security.DailyPrice.Last().Price) * 100;
        }

        private void DerivePriceToSalesTTM(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			if (!security.QuarterlySalesPerShare.Any() || !security.DailyPrice.Any()) {
				derivedSecurity.PriceToSalesRatioTTM = null;
				return;
			}

            var salesPerShareEntries = GetAllEntriesForTimeSpan(security.QuarterlySalesPerShare, TimePeriod.Year);

			if (salesPerShareEntries.Count() < 4)
			{
				derivedSecurity.PriceToSalesRatioTTM = null;
				return;
			}

            var yearlySalesPerShare = salesPerShareEntries.Sum(x => x.SalesPerShare);

            derivedSecurity.PriceToSalesRatioTTM = security.DailyPrice.Last().Price / yearlySalesPerShare;
        }

        private (TEntry present, TEntry past) GetEndpointDataForTimeRange<TEntry>(List<TEntry> timeEntries, TimePeriod range, double errorFactor = 0) where TEntry : TimeEntry
        {
            var mostRecent = timeEntries.Last();
            var mostRecentTime = mostRecent.Timestamp / 1000;
            var timeRangeInUnix = GetUnixFromTimePeriod(range);

            var past = timeEntries.FirstOrDefault(x => x.Timestamp > (mostRecentTime - timeRangeInUnix - errorFactor) && x.Timestamp < (mostRecentTime - timeRangeInUnix + errorFactor));

            return (mostRecent, past);
		}

		private void DerivePayoutRatio(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.PayoutRatio = security.PayoutRatio ?? null;
		}

		private void DeriveProfitMargin(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.ProfitMargin = security.ProfitMargin ?? null;
		}

		private void DeriveGrossMargin(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.GrossMargin = security.GrossMargin ?? null;
		}

		private void DeriveWorkingCapital(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.WorkingCapital = security.WorkingCapital ?? null;
		}

		private void DeriveDebtToEquityRatio(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.DebtToEquityRatio = security.DebtToEquityRatio ?? null;
		}

		private void DeriveFreeCashFlow(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.FreeCashFlow = security.FreeCashFlow ?? null;
		}

		private void DeriveCurrentRatio(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.CurrentRatio = security.CurrentRatio ?? null;
		}

		private void DeriveIndustry(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.Industry = security.Industry;
		}

		private void DeriveMarketCap(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.MarketCap = security.MarketCap;
		}

		private void DeriveSector(DerivedSecurity derivedSecurity, BaseSecurity security)
		{
			derivedSecurity.Sector = security.Sector;
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
