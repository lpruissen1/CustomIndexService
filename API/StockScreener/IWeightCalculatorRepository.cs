using StockScreener.Core;
using StockScreener.Core.Request;
using StockScreener.Model.Weighters;
using System.Collections.Generic;

namespace StockScreener
{
	public interface IWeightCalculatorRepository
	{
		WeightCalculator GetWeightCalculator(WeightingRequest request);
	}
}
