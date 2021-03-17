using StockScreener.Core;
using StockScreener.Model;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener
{
    public interface ISecuritiesGrabber
    {
        SecuritiesList GetSecurities(SecuritiesSearchParams searchParams);
    }
}
