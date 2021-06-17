using StockScreener.Model.BaseSecurity;

namespace StockScreener
{
	public interface IInclusionExclusionHandler
	{
		public void Apply(ref SecuritiesList<DerivedSecurity> securitiesList);
	}
}
