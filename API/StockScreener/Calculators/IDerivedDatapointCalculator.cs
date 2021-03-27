using StockScreener.Model.BaseSecurity;

namespace StockScreener.Calculators
{
    public interface IDerivedDatapointCalculator
    {
        SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities);
    }
}