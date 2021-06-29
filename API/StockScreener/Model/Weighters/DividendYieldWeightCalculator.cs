using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class DividendYieldWeightCalculator : WeightCalculator
	{
		public DividendYieldWeightCalculator(List<WeightingEntry> manualWeights) : base(manualWeights) { }
		public override List<WeightingEntry> Weight(SecuritiesList<DerivedSecurity> tickers)
		{
			return new List<WeightingEntry>();
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
