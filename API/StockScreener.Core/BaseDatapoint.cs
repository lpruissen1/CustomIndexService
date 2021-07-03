using System;

namespace StockScreener.Core
{
	[Flags]
    public enum BaseDatapoint
    {
		Ticker = 0,
        Sector = 1 << 0,
        Industry = 1 << 1,
        MarketCap = 1 << 2,
        Revenue = 1 << 3,
        QuarterlyEarningsPerShare = 1 << 4,
        Price = 1 << 5,
        PayoutRatio = 1 << 6, 
        ProfitMargin = 1 << 7,
        GrossMargin = 1 << 8,
        WorkingCapital = 1 << 9,
        DebtToEquityRatio = 1 << 10,
        FreeCashFlow = 1 << 11,
        CurrentRatio = 1 << 12,
        QuarterlySalesPerShare = 1 << 13,
        BookValuePerShare = 1 << 14,
        DividendsPerShare = 1 << 15,

        CompanyInfo  = Sector | Industry,
        StockFinancials  = MarketCap | Revenue | QuarterlyEarningsPerShare | PayoutRatio | ProfitMargin | GrossMargin | WorkingCapital | DebtToEquityRatio | FreeCashFlow | CurrentRatio | QuarterlySalesPerShare | BookValuePerShare | DividendsPerShare
    }
}
