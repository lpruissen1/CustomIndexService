using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Model.Weighters
{
	public class MarketCapWeightCalculator : WeightCalculator
	{
		public MarketCapWeightCalculator(Dictionary<string, decimal> manualWeights) : base(manualWeights) { }

		public override Dictionary<string, decimal> Weight(SecuritiesList<DerivedSecurity> tickers)
		{
			return new Dictionary<string, decimal>();
		}

		public override IEnumerable<BaseDatapoint> GetBaseDatapoint()
		{
			yield return BaseDatapoint.MarketCap;
		}

		public override IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData()
		{
			return Enumerable.Empty<DerivedDatapointConstructionData>();
		}
	}
}
