using Database.Core;
using StockScreener.Core;
using StockScreener.Database.Model.CompanyInfo;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface ICompanyInfoRepository : IBaseRepository<CompanyInfo>
	{
		CompanyInfo Get(string tickers, IEnumerable<BaseDatapoint> dataPoints);
		IEnumerable<CompanyInfo> GetMany(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> dataPoints);
		IEnumerable<string> GetAllTickers();
	}
}
