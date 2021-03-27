using StockScreener.Model.BaseSecurity;

namespace StockScreener.Calculators
{
    public class DerivedDatapointCalculator : IDerivedDatapointCalculator
    {
        public SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities)
        {
            return new SecuritiesList<DerivedSecurity>();
        }
    }
}
