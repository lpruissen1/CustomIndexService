﻿using System.Collections.Generic;

namespace UserCustomIndices.Model.Response
{
    public class CustomIndexResponse
    {
        public string Id { get; set; }
        public List<string> Markets { get; set; }
        public List<string> Sectors { get; set; }
        public List<string> Industries { get; set; }
        public List<Rule> Rules { get; init; } = new List<Rule>();

        //public List<DividendYield> DividendYields { get; init; } = new List<DividendYield>();
        //public List<CoefficientOfVariation> CoefficientOfVariation { get; init; } = new List<CoefficientOfVariation>();
        //public List<RevenueGrowthAnnualized> RevenueGrowthAnnualized { get; init; } = new List<RevenueGrowthAnnualized>();
        //public List<EPSGrowthAnnualized> EPSGrowthAnnualized { get; init; } = new List<EPSGrowthAnnualized>();
        //public List<AnnualizedTrailingPerformance> TrailingPerformanceAnnualized { get; init; } = new List<AnnualizedTrailingPerformance>();
        //public List<PriceToEarningsRatioTTM> PriceToEarningsRatioTTM { get; init; } = new List<PriceToEarningsRatioTTM>();
        //public List<PriceToSalesRatioTTM> PriceToSalesRatioTTM { get; init; } = new List<PriceToSalesRatioTTM>();
        //public List<Sector> SectorAndIndsutry { get; init; } = new List<Sector>();
        //public List<MarketCapitalization> MarketCaps { get; init; } = new List<MarketCapitalization>();
        public string Test { get; set; }
    }

    public class Rule
}
