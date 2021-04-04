using Core;
using Database.Model.User.CustomIndices;
using StockScreener.Core;
using StockScreener.Model.Metrics;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Mapper
{
    public class CustomIndexMapper : IMetricListMapper<CustomIndex>
    {
        public MetricList MapToMetricList(CustomIndex index)
        {
            MetricList metricList = new MetricList();
            metricList.Indices = index.Markets.ToArray();

            metricList.Add(MapSectorsAndIndustries(index.SectorAndIndsutry));
            metricList.Add(MapMarketCap(index.MarketCaps));
            metricList.Add(MapRevenueGrowth(index.RevenueGrowthAnnualized));
            metricList.Add(MapPriceToEarningsTTM(index.PriceToEarningsRatioTTM));
            metricList.Add(MapPayoutRatio(index.PayoutRatio));
            metricList.Add(MapProfitMargin(index.ProfitMargin));
            metricList.Add(MapGrossMargin(index.GrossMargin));
            metricList.Add(MapWorkingCapital(index.WorkingCapital));
            metricList.Add(MapDebtToEquityRatio(index.DebtToEquityRatio));
            metricList.Add(MapFreeCashFlow(index.FreeCashFlow));
            metricList.Add(MapCurrentRatio(index.CurrentRatio));
            metricList.Add(MapPriceToSalesTTM(index.PriceToSalesRatioTTM));
            metricList.Add(MapPriceToBook(index.PriceToBookValue));
            metricList.Add(MapDividendYield(index.DividendYields));
            metricList.Add(MapEPSGrowthAnnualized(index.EPSGrowthAnnualized));
            metricList.Add(MapTrailingPerformance(index.TrailingPerformance));

            return metricList;
        }

        private IMetric MapSectorsAndIndustries(List<Sector> sectors)
        {
            if (!sectors.Any())
                return null;

            var sectorList = new List<string>();
            var industryList = new List<string>();

            foreach(var sector in sectors)
            {
                if (sector.Industries is null)
                    sectorList.Add(sector.Name);

                else
                    industryList.AddRange(sector.Industries);
            }

            return new SectorAndIndustryMetric(sectorList, industryList);
        }

        private IMetric MapMarketCap(List<MarketCapitalization> marketCaps)
        {
            if(!marketCaps.Any())
                return null;

            var list = new List<Range>();

            foreach(var marketCapRange in marketCaps)
            {
                list.Add(new Range(marketCapRange.Upper, marketCapRange.Lower));
            }

            return new MarketCapMetric(list);
        }

        private IMetric MapWorkingCapital(List<WorkingCapitals> workingCapitals)
        {
            if (!workingCapitals.Any())
                return null;

            var list = new List<Range>();

            foreach (var workingCapitalRange in workingCapitals)
            {
                list.Add(new Range(workingCapitalRange.Upper, workingCapitalRange.Lower));
            }

            return new WorkingCapitalMetric(list);
        }

        private IMetric MapCurrentRatio(List<CurrentRatios> currentRatios)
        {
            if (!currentRatios.Any())
                return null;

            var list = new List<Range>();

            foreach (var currentRatiosRange in currentRatios)
            {
                list.Add(new Range(currentRatiosRange.Upper, currentRatiosRange.Lower));
            }

            return new CurrentRatioMetric(list);
        }

        private IMetric MapFreeCashFlow(List<FreeCashFlows> freeCashFlows)
        {
            if (!freeCashFlows.Any())
                return null;

            var list = new List<Range>();

            foreach (var freeCashFlowsRange in freeCashFlows)
            {
                list.Add(new Range(freeCashFlowsRange.Upper, freeCashFlowsRange.Lower));
            }

            return new FreeCashFlowMetric(list);
        }

        private IMetric MapDebtToEquityRatio(List<DebtToEquityRatios> debtToEquityRatios)
        {
            if (!debtToEquityRatios.Any())
                return null;

            var list = new List<Range>();

            foreach (var debtToEquityRatioRange in debtToEquityRatios)
            {
                list.Add(new Range(debtToEquityRatioRange.Upper, debtToEquityRatioRange.Lower));
            }

            return new DebtToEquityRatioMetric(list);
        }

        private IMetric MapPayoutRatio(List<PayoutRatios> payoutRatio)
        {
            if (!payoutRatio.Any())
                return null;

            var list = new List<Range>();

            foreach (var payoutRatioRange in payoutRatio)
            {
                list.Add(new Range(payoutRatioRange.Upper, payoutRatioRange.Lower));
            }

            return new PayoutRatioMetric(list);
        }

        private IMetric MapProfitMargin(List<ProfitMargins> profitMargins)
        {
            if (!profitMargins.Any())
                return null;

            var list = new List<Range>();

            foreach (var profitMarginRange in profitMargins)
            {
                list.Add(new Range(profitMarginRange.Upper, profitMarginRange.Lower));
            }

            return new ProfitMarginMetric(list);
        }

        private IMetric MapGrossMargin(List<GrossMargins> grossMargins)
        {
            if (!grossMargins.Any())
                return null;

            var list = new List<Range>();

            foreach (var grossMarginRange in grossMargins)
            {
                list.Add(new Range(grossMarginRange.Upper, grossMarginRange.Lower));
            }

            return new GrossMarginMetric(list);
        }

        private IMetric MapRevenueGrowth(List<RevenueGrowthAnnualized> revenueGrowth)
        {
            if(!revenueGrowth.Any())
                return null;

            var list = new List<RangeAndTimePeriod>();

            foreach(var revenueGrowthTarget in revenueGrowth)
            {
                list.Add(new RangeAndTimePeriod(new Range(revenueGrowthTarget.Upper, revenueGrowthTarget.Lower), GetTimeSpan(revenueGrowthTarget.TimePeriod)));
            }

            return new RevenueGrowthAnnualizedMetric(list);
        }

        private IMetric MapEPSGrowthAnnualized(List<EPSGrowthAnnualized> epsGrowth)
        {
            if (!epsGrowth.Any())
                return null;

            var list = new List<RangeAndTimePeriod>();

            foreach (var epsGrowthTarget in epsGrowth)
            {
                list.Add(new RangeAndTimePeriod(new Range(epsGrowthTarget.Upper, epsGrowthTarget.Lower), GetTimeSpan(epsGrowthTarget.TimePeriod)));
            }

            return new EPSGrowthAnnualizedMetric(list);
        }

        private IMetric MapTrailingPerformance(List<AnnualizedTrailingPerformance> trailingPerformances)
        {
            if (!trailingPerformances.Any())
                return null;

            var list = new List<RangeAndTimePeriod>();

            foreach (var trailingPerformance in trailingPerformances)
            {
                list.Add(new RangeAndTimePeriod(new Range(trailingPerformance.Upper, trailingPerformance.Lower), GetTimeSpan(trailingPerformance.TimePeriod)));
            }

            return new TrailingPerformanceMetric(list);
        }

        private IMetric MapPriceToEarningsTTM(List<PriceToEarningsRatioTTM> priceToEarningsRatioTTM)
        {
            if (!priceToEarningsRatioTTM.Any())
                return null;

            var list = new List<Range>();

            foreach (var priceToEarningsRatioTTMTarget in priceToEarningsRatioTTM)
            {
                list.Add(new Range(priceToEarningsRatioTTMTarget.Upper, priceToEarningsRatioTTMTarget.Lower));
            }

            return new PriceToEarningsRatioTTMMetric(list);
        }

        private IMetric MapDividendYield(List<DividendYield> dividendYield)
        {
            if (!dividendYield.Any())
                return null;

            var list = new List<Range>();

            foreach (var dividendYieldTarget in dividendYield)
            {
                list.Add(new Range(dividendYieldTarget.Upper, dividendYieldTarget.Lower));
            }

            return new DividendYieldMetric(list);
        }

        private IMetric MapPriceToBook(List<PriceToBookValue> priceToBookValue)
        {
            if (!priceToBookValue.Any())
                return null;

            var list = new List<Range>();

            foreach (var priceToBookValueTarget in priceToBookValue)
            {
                list.Add(new Range(priceToBookValueTarget.Upper, priceToBookValueTarget.Lower));
            }

            return new PriceToBookRatioMetric(list);
        }

        private IMetric MapPriceToSalesTTM(List<PriceToSalesRatioTTM> priceToSalesRatioTTM)
        {
            if (!priceToSalesRatioTTM.Any())
                return null;

            var list = new List<Range>();

            foreach (var priceToSalesRatioTTMTarget in priceToSalesRatioTTM)
            {
                list.Add(new Range(priceToSalesRatioTTMTarget.Upper, priceToSalesRatioTTMTarget.Lower));
            }

            return new PriceToSalesRatioTTMMetric(list);
        }
        private TimePeriod GetTimeSpan(int timeRange)
        {
            switch (timeRange)
            {
                case 1:
                    return TimePeriod.Quarter;
                case 2:
                    return TimePeriod.HalfYear;
                case 4:
                    return TimePeriod.Year;
                case 12:
                    return TimePeriod.ThreeYear;
                case 20:
                    return TimePeriod.FiveYear;
                default:
                    throw new System.Exception("Fuck yourself");
            }
        }
    }

    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
