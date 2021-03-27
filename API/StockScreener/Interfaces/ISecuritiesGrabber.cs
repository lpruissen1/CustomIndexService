using StockScreener.Core;
using StockScreener.Model;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener
{
    public interface ISecuritiesGrabber
    {
        SecuritiesList<BaseSecurity> GetSecurities(SecuritiesSearchParams searchParams);
    }
}
