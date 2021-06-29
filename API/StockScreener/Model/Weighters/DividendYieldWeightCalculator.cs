using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class DividendYieldWeightCalculator : WeightCalculator
	{
		public DividendYieldWeightCalculator(Dictionary<string, decimal> manualWeights) : base(manualWeights) { }
		public override Dictionary<string, decimal> Weight(SecuritiesList<DerivedSecurity> tickers)
		{
			return new Dictionary<string, decimal>();
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
