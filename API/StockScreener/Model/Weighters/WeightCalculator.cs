using StockScreener.Calculators;
using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Model.Weighters
{
	public abstract class WeightCalculator
	{
		protected List<WeightingEntry> ManualWeights { get; init; }

		public WeightCalculator (List<WeightingEntry> manualWeights)
		{
			ManualWeights = manualWeights;
		}

		public List<WeightingEntry> Weight(SecuritiesList<DerivedSecurity> tickers) 
		{
			if (tickers.Count == 0 && ManualWeights.Count == 0)
				return new List<WeightingEntry>();

			return Implementation(tickers);
		}

		public abstract List<WeightingEntry> Implementation(SecuritiesList<DerivedSecurity> tickers);

		public abstract IEnumerable<BaseDatapoint> GetBaseDatapoint();
		public abstract IEnumerable<DerivedDatapointConstructionData> GetDerivedDatapointConstructionData();

	}
}
