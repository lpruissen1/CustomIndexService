using StockScreener.Core.Request;
using StockScreener.Core.Response;

namespace StockScreener
{
	public interface IStockScreenerService
    {
        ScreeningResponse Screen(ScreeningRequest request);
		WeightingResponse Weighting(WeightingRequest request);

	}
}
