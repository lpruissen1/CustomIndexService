using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class DividendYieldWeightCalculator : WeightCalculator
	{
		public DividendYieldWeightCalculator(List<WeightingEntry> manualWeights) : base(manualWeights) { }
		public override List<WeightingEntry> Weight(SecuritiesList<DerivedSecurity> securities)
		{
			securities.RemoveAll(x => ManualWeights.Any(manualTicker => manualTicker.Ticker == x.Ticker));

			var weights = new List<WeightingEntry>();

			var remainingPercentage = 100 - ManualWeights.Sum(x => x.Weight);
			var individualPercentage = Math.Round(remainingPercentage / securities.Count, 5);

			var totalDividendYield = securities.Sum(x => x.DividendYield);

			foreach (var security in securities)
			{
				double individualWeight;

				// filter out the bad options before?
				if (security.DividendYield is not null)
				{
					individualWeight = Math.Round((double)(security.DividendYield / totalDividendYield) * remainingPercentage, 5);
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
			yield return BaseDatapoint.DividendsPerShare;
			yield return BaseDatapoint.Price;
		}

		public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData()
		{
			return Enumerable.Repeat(new DerivedDatapointConstructionData() { Datapoint = DerivedDatapoint.DividendYield }, 1);
		}
	}
}
