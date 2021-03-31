using Database.Core;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface ICompanyInfoRepository : IBaseRepository<CompanyInfo>
    {
        IEnumerable<CompanyInfo> Get(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> dataPoints);
        CompanyInfo Get(string tickers, IEnumerable<BaseDatapoint> dataPoints);
    }
}
