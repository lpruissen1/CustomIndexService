using StockScreener.Model.BaseSecurity;
using System.Collections.Generic;

namespace StockScreener.Calculators
{
    public interface IDerivedDatapointCalculator
    {
        SecuritiesList<DerivedSecurity> Derive(SecuritiesList<BaseSecurity> securities, IEnumerable<DerivedDatapointConstructionData> derivedDatapoints);
    }
}