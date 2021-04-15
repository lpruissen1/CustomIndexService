using Database.Model.User.CustomIndices;
using StockScreener.Model.BaseSecurity;
using UserCustomIndices.Model.Response;

namespace StockScreener
{
    public interface IStockScreenerService
    {
        SecuritiesList<DerivedSecurity> Screen(CustomIndex index);
        SecuritiesList<DerivedSecurity> Screen(CustomIndexResponse index);
    }
}