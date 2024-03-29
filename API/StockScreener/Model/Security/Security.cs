﻿namespace StockScreener.Model.BaseSecurity
{
    public abstract class Security
    {
        public string Ticker;
        public string Name;
        public string Sector;
        public string Industry;
        public double? MarketCap;
        public double? PayoutRatio;
        public double? ProfitMargin;
        public double? GrossMargin;
        public double? WorkingCapital;
        public double? DebtToEquityRatio;
        public double? FreeCashFlow;
        public double? CurrentRatio;
        public double? BookValuePerShare;
        public double? CurrentPrice;
    }
}
