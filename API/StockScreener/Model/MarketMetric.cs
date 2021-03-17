using StockScreener.Core;
using System.Collections.Generic;

namespace StockScreener
{
    public class MarketMetric
    {
        public string[] markets { get; init; }

        public void Apply(SecuritiesList securitiesList)
        {
            throw new System.Exception("I shouldnt be called");
        }

        public Datapoint GetRelevantDatapoint()
        {
            throw new System.NotImplementedException();
        }
    }
}
