using StockScreener.Core;
using StockScreener.Core.Request;
using StockScreener.Model.Weighters;

namespace StockScreener
{
	public class WeightCalculatorRepository : IWeightCalculatorRepository
	{
		public WeightCalculator GetWeightCalculator(WeightingRequest request)
		{
			switch (request.Option)
			{
				case WeightingOption.DividendYield:
					return new DividendYieldWeightCalculator(request.ManualWeights);

				case WeightingOption.MarketCap:
					return new MarketCapWeightCalculator(request.ManualWeights);

				default:
					return new EqualWeightCalculator(request.ManualWeights);
			}
		}
	}
}
