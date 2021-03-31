﻿using StockScreener.Core;
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
                {BaseDatapoint.PayoutRatio, AddPayoutRatio }
            };
        }

        private void AddRevenue()
        {
            projection.Include(x => x.Revenues);
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
    }
}