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
        QuarterlyEarningsPerShare = 16,
        Price = 32,
        PayoutRatio = 64, 
        ProfitMargin = 128,
        GrossMargin = 256,
        WorkingCapital = 512,
        DebtToEquityRatio = 1024,
        FreeCashFlow = 2048,
        CurrentRatio = 4096,
        QuarterlySalesPerShare = 8192,
        BookValuePerShare = 16384,
        DividendsPerShare = 32768,


        CompanyInfo  = Sector | Industry,
        StockFinancials  = MarketCap | Revenue | QuarterlyEarningsPerShare | PayoutRatio | ProfitMargin | GrossMargin | WorkingCapital | DebtToEquityRatio | FreeCashFlow | CurrentRatio | QuarterlySalesPerShare | BookValuePerShare | DividendsPerShare
    }
}
