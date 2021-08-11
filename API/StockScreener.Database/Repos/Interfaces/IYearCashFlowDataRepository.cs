using Database.Core;
using StockScreener.Database.Model;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IYearCashFlowDataRepository : IBaseRepository<YearCashFlowData>
	{
		void Load(IEnumerable<YearCashFlowData> data);
	}
}
