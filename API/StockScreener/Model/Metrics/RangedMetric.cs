using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public abstract class RangedMetric : IMetric
    {
        public RangedMetric(List<Range> ranges)
        {
            rangedDatapoint = ranges;
        }

        private List<Range> rangedDatapoint { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !rangedDatapoint.Any(range => range.WithinRange(GetValue(security))));
        }

        public abstract double? GetValue(DerivedSecurity security);

        public abstract IEnumerable<BaseDatapoint> GetBaseDatapoints();

        public abstract IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints();

		public abstract TimePeriod? GetPriceTimePeriod();
	}
}
