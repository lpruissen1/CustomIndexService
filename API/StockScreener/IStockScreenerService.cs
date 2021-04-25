using StockScreener.Model.BaseSecurity;
using UserCustomIndices.Model.Response;

namespace StockScreener
{
    public interface IStockScreenerService
    {
        SecuritiesList<DerivedSecurity> Screen(CustomIndexResponse index);
    }
}