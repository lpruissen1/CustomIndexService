using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;

namespace Database.Repositories
{
    public class StockFinancialsProjectionBuilder : ProjectionBuilder<StockFinancials>
    {
        public StockFinancialsProjectionBuilder() : base()
        {
            datapointMapper = new Dictionary<BaseDatapoint, Action>()
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
                {BaseDatapoint.DividendsPerShare, AddDividendsPerShare },
            };
        }

        private void AddRevenue()
        {
            projection.Include(x => x.Revenues);
        }

        private void AddCurrentRatio()
        {
            projection.Include(x => x.CurrentRatio);
        }

        private void AddDividendsPerShare()
        {
            projection.Include(x => x.DividendsPerShare);
        }

        private void AddBookValuePerShare()
        {
            projection.Include(x => x.BookValuePerShare);
        }

        private void AddFreeCashFlow()
        {
            projection.Include(x => x.FreeCashFlow);
        }

        private void AddDebtToEquityRatio()
        {
            projection.Include(x => x.DebtToEquityRatio);
        }

        private void AddProfitMargin()
        {
            projection.Include(x => x.ProfitMargin);
        }

        private void AddWorkingCapital()
        {
            projection.Include(x => x.WorkingCapital);
        }

        private void AddGrossMargin()
        {
            projection.Include(x => x.GrossMargin);
        }

        private void AddPayoutRatio()
        {
            projection.Include(x => x.PayoutRatio);
        }

        private void AddMarketCap()
        {
            projection.Include(x => x.MarketCap);
        }

        private void AddQuarterlyEarningsPerShare()
        {
            projection.Include(x => x.EarningsPerShare);
        }

        private void AddQuarterlySalesPerShare()
        {
            projection.Include(x => x.SalesPerShare);
        }
    }
}