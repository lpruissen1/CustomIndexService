using Core;
using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Metrics
{
    public interface IMetric
    {
        void Apply(ref SecuritiesList<DerivedSecurity> securitiesList);

        // change to return a a struct for metrics that specify a timerange 
        IEnumerable<BaseDatapoint> GetBaseDatapoints();
        IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapoints();
		TimePeriod? GetPriceTimePeriod();
	}
}

