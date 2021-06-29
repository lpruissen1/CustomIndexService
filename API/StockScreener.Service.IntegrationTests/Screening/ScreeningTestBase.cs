using Core;
using NUnit.Framework;
using StockScreener.Core.Request;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
	public class ScreeningTestBase : StockScreenerServiceTestBase
	{
		protected ScreeningRequest screeningRequest;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();
			screeningRequest = new ScreeningRequest();
		}

		public void AddMarketToScreeningRequest(string market)
		{
			screeningRequest.Markets.Add(market);
		}

		public void AddIncludedTickerToScreeningRequest(string ticker)
		{
			screeningRequest.Inclusions.Add(ticker);
		}

		public void AddExcludedTickerToScreeningRequest(string ticker)
		{
			screeningRequest.Exclusions.Add(ticker);
		}

		public void AddSectorAndIndustryToScreeningRequest(List<string> sector, List<string> industry)
		{
			screeningRequest.Industries = industry;
			screeningRequest.Sectors = sector;
		}

		public void AddPriceToEarningsRatioToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.PriceToEarningsRatioTTM });
		}

		public void AddDividendYieldToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.DividendYield });
		}

		public void AddPriceToSalesRatioToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.PriceToSalesRatioTTM });
		}

		public void AddMarketCapToScreeningRequest(double upper, double lower)
		{
			screeningRequest.RangedRule.Add(new RangedRule { Upper = upper, Lower = lower, RuleType = RuleType.MarketCap });
		}

		public void AddAnnualizedRevenueGrowthToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
		{
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.RevenueGrowthAnnualized });
		}

		public void AddAnnualizedEPSGrowthToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
		{
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.EPSGrowthAnnualized });
		}

		public void AddAnnualizedTrailingPerformanceoScreeningRequest(double upper, double lower, TimePeriod timePeriod)
		{
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.AnnualizedTraillingPerformance });
		}

		public void AddCoefficientOfVariationToScreeningRequest(double upper, double lower, TimePeriod timePeriod)
		{
			screeningRequest.TimedRangeRule.Add(new TimedRangeRule { Upper = upper, Lower = lower, TimePeriod = timePeriod, RuleType = RuleType.CoefficientOfVariation });
		}
	}
}
