using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public abstract class RangeAndTimeSpanMetric : IMetric
    {
        public RangeAndTimeSpanMetric(List<RangeAndTimeSpan> rangesAndTimeSpans)
        {
            rangedDatapoint = rangesAndTimeSpans;
        }

        protected List<RangeAndTimeSpan> rangedDatapoint { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !rangedDatapoint.Any(entry => entry.Valid(GetValue(security)[entry.GetTimeSpan()])));
        }

        public abstract Dictionary<TimeSpan, double> GetValue(DerivedSecurity security);

        public abstract IEnumerable<BaseDatapoint> GetBaseDatapoints();

        public abstract IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints();
    }
}