using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class EqualWeightCalculator : WeightCalculator
	{
		public EqualWeightCalculator(Dictionary<string, decimal> manualWeights) : base(manualWeights) { }
		public override Dictionary<string, decimal> Weight(SecuritiesList<DerivedSecurity> tickers)
		{
			var weights = ManualWeights;
			var remainingPercentage = 100m - weights.Sum(x => x.Value);
			var individualPercentage = Math.Round(remainingPercentage / tickers.Count, 5);

			foreach (var ticker in tickers)
			{
				weights.Add(ticker.Ticker, individualPercentage);
			}

			var currentWeight = weights.Sum(x => x.Value);

			if (currentWeight != 100m)
				weights[weights.First().Key] += 100m - currentWeight;

			return weights;
		}

		public override IEnumerable<BaseDatapoint> GetBaseDatapoint()
		{
			return Enumerable.Empty<BaseDatapoint>();
		}

		public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData()
		{
			return Enumerable.Empty<DerivedDatapointConstructionData>();
		}
	}
}
