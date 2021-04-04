using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Model.BaseSecurity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.SecurityGrabber.BaseDataMapper
{
    public class StockFinancialsMapper : SecurityMapper<StockFinancials>
    {
        public StockFinancialsMapper()
        {
            datapointMapperDictionary = new Dictionary<BaseDatapoint, Action<StockFinancials>>()
            {
                {BaseDatapoint.Revenue, AddRevenue },
                {BaseDatapoint.MarketCap, AddMarketCap },
                {BaseDatapoint.QuarterlyEarningsPerShare, AddQuarterlyEarningsPerShare },
                {BaseDatapoint.PayoutRatio, AddPayoutRatio },
                {BaseDatapoint.ProfitMargin, AddProfitMargin },
                {BaseDatapoint.GrossMargin, AddGrossMargin },
                {BaseDatapoint.WorkingCapital, AddWorkingCapital },
                {BaseDatapoint.DebtToEquityRatio, AddDebtToEquityRatio },
                {BaseDatapoint.FreeCashFlow, AddFreeCashFlow },
                {BaseDatapoint.CurrentRatio, AddCurrentRatio },
                {BaseDatapoint.QuarterlySalesPerShare, AddQuarterlySalesPerShare },
                {BaseDatapoint.BookValuePerShare, AddBookValuePerShare },
                {BaseDatapoint.DividendsPerShare, AddQuarterlyDividendsPerShare }
            };
        }

        public void AddRevenue(StockFinancials stockFinancials)
        {
            var present = stockFinancials.Revenues.Last();

            security.QuarterlyRevenue = stockFinancials.Revenues.Select(stock => new RevenueEntry() { Revenue = stock.revenues, Timestamp = stock.timestamp}).ToList();
        }

        public void AddMarketCap(StockFinancials stockFinancials)
        {
            security.MarketCap = stockFinancials.MarketCap.Last().marketCap;
        }

        public void AddCurrentRatio(StockFinancials stockFinancials)
        {
            security.CurrentRatio = stockFinancials.CurrentRatio.Last().currentRatio;
        }

        public void AddBookValuePerShare(StockFinancials stockFinancials)
        {
            security.BookValuePerShare = stockFinancials.BookValuePerShare.Last().bookValuePerShare;
        }

        public void AddFreeCashFlow(StockFinancials stockFinancials)
        {
            security.FreeCashFlow = stockFinancials.FreeCashFlow.Last().freeCashFlow;
        }

        public void AddDebtToEquityRatio(StockFinancials stockFinancials)
        {
            security.DebtToEquityRatio = stockFinancials.DebtToEquityRatio.Last().debtToEquityRatio;
        }

        public void AddGrossMargin(StockFinancials stockFinancials)
        {
            security.GrossMargin = stockFinancials.GrossMargin.Last().grossMargin;
        }

        public void AddWorkingCapital(StockFinancials stockFinancials)
        {
            security.WorkingCapital = stockFinancials.WorkingCapital.Last().workingCapital;
        }

        public void AddProfitMargin(StockFinancials stockFinancials)
        {
            security.ProfitMargin = stockFinancials.ProfitMargin.Last().profitMargin;
        }

        public void AddPayoutRatio(StockFinancials stockFinancials)
        {
            security.PayoutRatio = stockFinancials.PayoutRatio.Last().payoutRatio;
        }

        public void AddQuarterlyEarningsPerShare(StockFinancials stockFinancials)
        {
            security.QuarterlyEarnings = stockFinancials.EarningsPerShare.Select(stock => new EarningsEntry() { Earnings = stock.earningsPerShare, Timestamp = stock.timestamp }).ToList();
        }

        public void AddQuarterlyDividendsPerShare(StockFinancials stockFinancials)
        {
            security.QuarterlyDividendsPerShare = stockFinancials.DividendsPerShare.Select(stock => new DividendEntry() { QuarterlyDividends = stock.dividendsPerShare, Timestamp = stock.timestamp }).ToList();
        }

        public void AddQuarterlySalesPerShare(StockFinancials stockFinancials)
        {
            security.QuarterlySalesPerShare = stockFinancials.SalesPerShare.Select(stock => new SalesPerShareEntry() { SalesPerShare = stock.salesPerShare, Timestamp = stock.timestamp }).ToList();
        }
    }

}
