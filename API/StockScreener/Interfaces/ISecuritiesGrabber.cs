using StockScreener.Model.BaseSecurity;
using StockScreener.SecurityGrabber;

namespace StockScreener.Interfaces
{
	public interface ISecuritiesGrabber
	{
		SecuritiesList<BaseSecurity> GetSecurities(SecuritiesSearchParams searchParams);
	}
}
