using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Metrics
{
    public abstract class RangeAndTimPeriodMetric : IMetric
    {
        public RangeAndTimPeriodMetric(List<RangeAndTimePeriod> rangesAndTimeSpans)
        {
            rangedDatapoint = rangesAndTimeSpans;
        }

        protected List<RangeAndTimePeriod> rangedDatapoint { get; init; }

        public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList)
        {
            securitiesList.RemoveAll(security => !rangedDatapoint.Any(entry => entry.Valid(GetValue(security)[entry.GetTimePeriod()])));
        }

        public abstract Dictionary<TimePeriod, double> GetValue(DerivedSecurity security);

        public abstract IEnumerable<BaseDatapoint> GetBaseDatapoints();

        public abstract IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints();
    }
}