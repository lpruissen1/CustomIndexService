using Database.Core;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System.Collections.Generic;

namespace StockScreener.Database.Repos
{
    public interface ICompanyInfoRepository : IBaseRepository<CompanyInfo>
    {
        CompanyInfo Get(string tickers, IEnumerable<BaseDatapoint> dataPoints);
    }
}
