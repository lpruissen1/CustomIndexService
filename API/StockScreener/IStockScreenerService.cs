using Database.Model.User.CustomIndices;
using StockScreener.Model.BaseSecurity;

namespace StockScreener
{
    public interface IStockScreenerService
    {
        SecuritiesList<DerivedSecurity> Screen(CustomIndex index);
    }
}