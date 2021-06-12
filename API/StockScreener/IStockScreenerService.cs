using StockScreener.Core.Request;
using StockScreener.Model.BaseSecurity;

namespace StockScreener
{
    public interface IStockScreenerService
    {
        SecuritiesList<DerivedSecurity> Screen(ScreeningRequest request);
    }
}
