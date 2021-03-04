using System;

namespace StockScreener.Core
{
    [Flags]
    public enum Datapoint
    {
        SectorAndIndustry = 0,

        CompanyInfo  = SectorAndIndustry
    }
}
