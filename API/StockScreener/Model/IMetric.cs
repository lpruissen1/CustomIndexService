using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public interface IMetric
    {
        SecuritiesList Apply(SecuritiesList securitiesList);
    }
}
