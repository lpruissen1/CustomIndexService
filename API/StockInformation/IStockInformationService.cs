using System.Collections.Generic;

namespace StockInformation
{
	public interface IStockInformationService
	{
		IEnumerable<string> GetAllTickers();
	}
}
