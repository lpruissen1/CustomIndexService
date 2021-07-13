using StockScreener.Core.Request;
using StockScreener.Core.Response;
using StockScreener.Model.BaseSecurity;

namespace StockScreener
{
	public interface IStockScreenerService
    {
		SecuritiesList<DerivedSecurity> Screen(ScreeningRequest request);
		WeightingResponse Weighting(WeightingRequest request);

	}
}
