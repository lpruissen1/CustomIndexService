using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class MarketCapWeightCalculator : WeightCalculator
	{
		public MarketCapWeightCalculator(Dictionary<string, decimal> manualWeights) : base(manualWeights) { }

		public override Dictionary<string, decimal> Weight(SecuritiesList<DerivedSecurity> tickers)
		{
			tickers.RemoveAll(x => ManualWeights.Any(manualTicker => manualTicker.Key == x.Ticker));

			var weights = ManualWeights;
			var remainingPercentage = 100m - weights.Sum(x => x.Value);

			decimal totalMarketCap = (decimal)tickers.Sum(x => x.MarketCap);

			foreach (var ticker in tickers)
			{
				var individualWeight = Math.Round((decimal)ticker.MarketCap / totalMarketCap * remainingPercentage, 5);
				weights.Add(ticker.Ticker, individualWeight);
			}

			var currentWeight = weights.Sum(x => x.Value);

			if (currentWeight != 100m)
				weights[weights.First().Key] += 100m - currentWeight;

			return weights;
		}

		public override IEnumerable<BaseDatapoint> GetBaseDatapoint()
		{
			yield return BaseDatapoint.MarketCap;
		}

		public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData()
		{
			yield return new DerivedDatapointConstructionData { Datapoint = DerivedDatapoint.MarketCap };
		}
	}
}
