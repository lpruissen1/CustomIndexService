using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public class MarketMetric : IMetric
    {
        public string[] markets { get; init; }

        public SecuritiesList Apply(SecuritiesList securitiesList)
        {
            throw new System.Exception("I shouldnt be called");
        }
    }
}
