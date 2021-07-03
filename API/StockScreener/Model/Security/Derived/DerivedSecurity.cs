using Core;
using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
	public class DerivedSecurity : Security
    {
		public Dictionary<TimePeriod, double?> RevenueGrowthAnnualized { get; init; } = new Dictionary<TimePeriod, double?>();

		public Dictionary<TimePeriod, double?> EPSGrowthAnnualized { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> RevenueGrowthRaw { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> EPSGrowthRaw { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> DividendGrowthAnnualized { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> DividendGrowthRaw { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> TrailingPerformanceAnnualized { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> TrailingPerformanceRaw { get; init; } = new Dictionary<TimePeriod, double?>();
		public Dictionary<TimePeriod, double?> CoefficientOfVariation { get; init; } = new Dictionary<TimePeriod, double?>();
		public double? PriceToEarningsRatioTTM;
        public double? PriceToSalesRatioTTM;
        public double? PriceToBookValue;
        public double? DividendYield;
    }

}
