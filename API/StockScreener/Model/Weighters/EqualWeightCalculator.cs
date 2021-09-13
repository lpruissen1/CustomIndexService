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
		public EqualWeightCalculator(List<WeightingEntry> manualWeights) : base(manualWeights) { }
		public override List<WeightingEntry> Implementation(SecuritiesList<DerivedSecurity> tickers)
		{
			tickers.RemoveAll(x => ManualWeights.Any(manualTicker => manualTicker.Ticker == x.Ticker));

			var weights = new List<WeightingEntry>();

			var remainingPercentage = 100 - ManualWeights.Sum(x => x.Weight);
			var individualPercentage = Math.Round(remainingPercentage / tickers.Count, 5);

			foreach (var ticker in tickers)
			{
				weights.Add(new WeightingEntry(ticker.Ticker, individualPercentage));
			}

			var currentWeight = weights.Sum(x => x.Weight);

			if (currentWeight != remainingPercentage)
				weights[0].Weight += remainingPercentage - currentWeight;

			weights.AddRange(ManualWeights);

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
