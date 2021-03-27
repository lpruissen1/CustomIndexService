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
        EarningsPerShare = 16,
        CurrentPrice = 32,

        CompanyInfo  = Sector | Industry,
        StockFinancials  = MarketCap | Revenue,
        Price  = CurrentPrice
    }
}
