using Database.Model.StockData;
using StockScreener.Core;
using System.Collections.Generic;

namespace Database.Repositories
{
    public interface ICompanyInfoRepository : IBaseRepository<CompanyInfo>
    {
        IEnumerable<CompanyInfo> Get(IEnumerable<Datapoint> dataPoints);
    }
}
