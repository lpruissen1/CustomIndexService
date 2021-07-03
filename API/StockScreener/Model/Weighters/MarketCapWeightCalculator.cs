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
		public MarketCapWeightCalculator(List<WeightingEntry> manualWeights) : base(manualWeights) { }

		public override List<WeightingEntry> Weight(SecuritiesList<DerivedSecurity> securities)
		{
			securities.RemoveAll(security => ManualWeights.Any(manualTicker => manualTicker.Ticker == security.Ticker));

			var weights = new List<WeightingEntry>();
			var remainingPercentage = (100 - ManualWeights.Sum(x => x.Weight));

			var totalMarketCap = securities.Sum(x => x.MarketCap);

			foreach (var security in securities)
			{
				double individualWeight;

				// filter out the bad options before?
				if (security.MarketCap is not null)
				{
					individualWeight = Math.Round((double)(security.MarketCap / totalMarketCap) * remainingPercentage, 5);
				}
				else
				{
					individualWeight = 0;
				}

				weights.Add(new WeightingEntry(security.Ticker, individualWeight));
			}

			var currentWeight = weights.Sum(x => x.Weight);

			if (currentWeight != remainingPercentage)
				weights[0].Weight += remainingPercentage - currentWeight;

			weights.AddRange(ManualWeights);

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
