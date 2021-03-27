using System;

namespace StockScreener.Core
{
    [Flags]
    public enum BaseDatapoint
    {
        Sector = 1,
        Industry = 2,
        MarketCap = 4,
        Revenue = 8,
        Price = 32,

        CompanyInfo  = Sector | Industry,
        StockFinancials  = MarketCap | Revenue,
    }
}
