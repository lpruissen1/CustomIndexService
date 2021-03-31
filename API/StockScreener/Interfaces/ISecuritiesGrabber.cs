using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;

namespace StockScreener
{
    public interface ISecuritiesGrabber
    {
        SecuritiesList<BaseSecurity> GetSecurities(SecuritiesSearchParams searchParams);
    }
}
