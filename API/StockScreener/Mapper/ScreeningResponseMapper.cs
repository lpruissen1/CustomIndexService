using Core;
using StockScreener.Calculators;
using StockScreener.Core.Response;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Mapper
{
	public class ScreeningResponseMapper
	{
		private Dictionary<RuleType, Func<DerivedSecurity, TimePeriod, double>> ruleMapper;

		public ScreeningResponseMapper()
		{
			ruleMapper = new Dictionary<RuleType, Func<DerivedSecurity, TimePeriod, double>>
			{
				{RuleType.DividendYield, MapDividendYield },
				{RuleType.CoefficientOfVariation, MapCoefficientOfVariation },
				{RuleType.RevenueGrowthAnnualized, MapRevenueGrowthAnnualized },
				{RuleType.EPSGrowthAnnualized, MapEPSGrowthAnnualized },
				{RuleType.AnnualizedTrailingPerformance, MapTrailingPerformanceAnnualized },
				{RuleType.PriceToEarningsRatioTTM, MapPriceToEarningsRatioTTM },
				{RuleType.PriceToSalesRatioTTM, MapPriceToSalesRatioTTM }
			};
		}

		public ScreeningResponse Map(SecuritiesList<DerivedSecurity> result, IEnumerable<DerivedDatapointConstructionData> screeningRules)
        {
			return new ScreeningResponse
			{
				Securities = result.Select(result => MapFilteredFields(result, screeningRules)).ToList()
			};
        }

		private ScreeningEntry MapFilteredFields(DerivedSecurity security, IEnumerable<DerivedDatapointConstructionData> filterParams)
		{
			ScreeningEntry entry = new ScreeningEntry(security.Ticker);

			entry.Sector = security.Sector;
			entry.Industry = security.Industry ?? "";
			entry.Name = security.Name;
			entry.MarketCap = security?.MarketCap ?? 0;
			entry.CurrentPrice = security?.CurrentPrice ?? 0;

			foreach(var rule in filterParams.Where(x => x.Rule != RuleType.SectorIndustry && x.Rule != RuleType.MarketCap)) {
				entry.ScreeningParameterValues.Add(new ScreeningParamValue
				{
					TimePeriod = rule.Time,
					RuleType = rule.Rule,
					Value = ruleMapper[rule.Rule].Invoke(security, rule.Time)
				});
			}
			return entry;
		}

		private double MapDividendYield(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.DividendYield.Value;
		}

		private double MapCoefficientOfVariation(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.CoefficientOfVariation[timePeriod].Value;
		}

		private double MapRevenueGrowthAnnualized(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.RevenueGrowthAnnualized[timePeriod].Value;
		}

		private double MapEPSGrowthAnnualized(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.EPSGrowthAnnualized[timePeriod].Value;
		}

		private double MapTrailingPerformanceAnnualized(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.TrailingPerformanceAnnualized[timePeriod].Value;
		}

		private double MapPriceToEarningsRatioTTM(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.PriceToEarningsRatioTTM.Value;
		}

		private double MapPriceToSalesRatioTTM(DerivedSecurity security, TimePeriod timePeriod)
		{
			return security.PriceToSalesRatioTTM.Value;
		}

	}
}
