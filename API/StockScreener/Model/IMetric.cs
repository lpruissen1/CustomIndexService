using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public interface IMetric
    {
        void Apply(ref SecuritiesList securitiesList);
        IEnumerable<Datapoint> GetRelevantDatapoints();
    }
}
