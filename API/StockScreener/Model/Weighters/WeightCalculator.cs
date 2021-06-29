using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Weighters
{
	public abstract class WeightCalculator
	{
		protected Dictionary<string, decimal> ManualWeights { get; init; }
		public WeightCalculator (Dictionary<string, decimal> manualWeights)
		{
			ManualWeights = manualWeights;
		}

		public abstract Dictionary<string, decimal> Weight(SecuritiesList<DerivedSecurity> tickers);

		public abstract IEnumerable<BaseDatapoint> GetBaseDatapoint();
		public abstract IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData();

	}
}
