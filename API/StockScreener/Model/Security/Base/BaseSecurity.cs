﻿using System.Collections.Generic;

namespace StockScreener.Model.BaseSecurity
{
    public class BaseSecurity : Security
    {
        public List<EarningsEntry> QuarterlyEarnings = new List<EarningsEntry>();
        public List<RevenueEntry> QuarterlyRevenue = new List<RevenueEntry>();
		public List<PriceEntry> DailyPrice = new List<PriceEntry>();
		public List<SalesPerShareEntry> QuarterlySalesPerShare = new List<SalesPerShareEntry>();
		public List<DividendEntry> QuarterlyDividendsPerShare = new List<DividendEntry>();

		public void Map(BaseSecurity security)
        {
            Sector = security.Sector != null ? security.Sector : Sector;
            Name = security.Name != null ? security.Name : Name;
            Industry = security.Industry != null ? security.Industry : Industry;
            MarketCap = security.MarketCap != 0 ? security.MarketCap : MarketCap;
            QuarterlyEarnings = security.QuarterlyEarnings != null ? security.QuarterlyEarnings : QuarterlyEarnings;
            QuarterlyRevenue = security.QuarterlyRevenue != null ? security.QuarterlyRevenue : QuarterlyRevenue;
            MarketCap = security.MarketCap != 0 ? security.MarketCap : MarketCap;
            PayoutRatio = security.PayoutRatio != 0 ? security.PayoutRatio : PayoutRatio;
            ProfitMargin = security.ProfitMargin != 0 ? security.ProfitMargin : ProfitMargin;
            GrossMargin = security.GrossMargin != 0 ? security.GrossMargin : GrossMargin;
            WorkingCapital = security.WorkingCapital != 0 ? security.WorkingCapital : WorkingCapital;
            DebtToEquityRatio = security.DebtToEquityRatio != 0 ? security.DebtToEquityRatio : DebtToEquityRatio;
            FreeCashFlow = security.FreeCashFlow != 0 ? security.FreeCashFlow : FreeCashFlow;
            CurrentRatio = security.CurrentRatio != 0 ? security.CurrentRatio : CurrentRatio;
            QuarterlySalesPerShare = security.QuarterlySalesPerShare != null ? security.QuarterlySalesPerShare : QuarterlySalesPerShare;
            BookValuePerShare = security.BookValuePerShare != 0 ? security.BookValuePerShare : BookValuePerShare;
            QuarterlyDividendsPerShare = security.QuarterlyDividendsPerShare != null ? security.QuarterlyDividendsPerShare : QuarterlyDividendsPerShare;
        }
    }

}
