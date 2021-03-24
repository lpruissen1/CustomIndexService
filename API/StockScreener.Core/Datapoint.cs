using System;

namespace StockScreener.Core
{
    [Flags]
    public enum Datapoint
    {
        Sector = 1,
        Industry = 2,
        MarketCap = 4,
        Revenue = 8,

        CompanyInfo  = Sector | Industry,
        StockFinancials  = MarketCap | Revenue
    }
}
