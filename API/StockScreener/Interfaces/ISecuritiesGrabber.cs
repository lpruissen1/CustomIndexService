using StockScreener.Core;
using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;
using System.Collections.Generic;

namespace StockScreener
{
    public interface ISecuritiesGrabber
    {
        SecuritiesList<BaseSecurity> GetSecurities(SecuritiesSearchParams searchParams);
        SecuritiesList<BaseSecurity> GetSecurities(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoint);
    }
}
