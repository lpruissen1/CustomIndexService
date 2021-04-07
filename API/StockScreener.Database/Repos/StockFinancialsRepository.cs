﻿using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Core;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.Repos
{
    public class StockFinancialsRepository : BaseRepository<StockFinancials>, IStockFinancialsRepository
    {

        private StockFinancialsProjectionBuilder projectionBuilder = new StockFinancialsProjectionBuilder();
        public StockFinancialsRepository(IMongoDBContext context) : base(context) { }

        public IEnumerable<StockFinancials> Get(IEnumerable<string> tickers, IEnumerable<BaseDatapoint> datapoints)
        {
            var projection = Builders<StockFinancials>.Projection.Include(x => x.Ticker).Include(x => x.MarketCap);

            if (datapoints.Contains(BaseDatapoint.MarketCap))
            {
                projection.Include(x => x.MarketCap.Last());
            }

            return dbCollection.Find(x => tickers.Contains(x.Ticker)).Project<StockFinancials>(projection).ToEnumerable();
        }

        public StockFinancials Get(string ticker, IEnumerable<BaseDatapoint> datapoints)
        {
            var projection = projectionBuilder.BuildProjection(datapoints);

            return dbCollection.Find(x => ticker == x.Ticker).Project<StockFinancials>(projection).FirstOrDefault();
        }

        public void Update(StockFinancials entry)
        {
            var filter = Builders<StockFinancials>.Filter.Eq(e => e.Ticker, entry.Ticker);

            var updateDefinition = new List<UpdateDefinition<StockFinancials>>();

            updateDefinition.Add(AddMarketCapUpdate(entry.MarketCap));
            updateDefinition.Add(AddDividendsPerShareUpdate(entry.DividendsPerShare));
            updateDefinition.Add(AddEarningsPerShareUpdate(entry.EarningsPerShare));
            updateDefinition.Add(AddSalesPerShareUpdate(entry.SalesPerShare));
            updateDefinition.Add(AddBookValuePerShareUpdate(entry.BookValuePerShare));
            updateDefinition.Add(AddPayoutRatioUpdate(entry.PayoutRatio));
            updateDefinition.Add(AddCurrentRatioUpdate(entry.CurrentRatio));
            updateDefinition.Add(AddDebtToEquityRatioUpdate(entry.DebtToEquityRatio));
            updateDefinition.Add(AddEnterpriseValueUpdate(entry.EnterpriseValue));
            updateDefinition.Add(AddEBITDAUpdate(entry.EBITDA));
            updateDefinition.Add(AddFreeCashFlowUpdate(entry.FreeCashFlow));
            updateDefinition.Add(AddGrossMarginUpdate(entry.GrossMargin));
            updateDefinition.Add(AddProfitMarginUpdate(entry.ProfitMargin));
            updateDefinition.Add(AddRevenuesUpdate(entry.Revenues));
            updateDefinition.Add(AddWorkingCapitalUpdate(entry.WorkingCapital));


            var combinedUpdate = Builders<StockFinancials>.Update.Combine(updateDefinition);
            var updated = dbCollection.FindOneAndUpdate(filter, combinedUpdate);
        }

        private UpdateDefinition<StockFinancials> AddMarketCapUpdate(List<MarketCap> marketCaps)
        {
            return Builders<StockFinancials>.Update.PushEach("MarketCap", marketCaps);
        }

        private UpdateDefinition<StockFinancials> AddDividendsPerShareUpdate(List<DividendsPerShare> dividendsPerShares)
        {
            return Builders<StockFinancials>.Update.PushEach("DividendsPerShare", dividendsPerShares);
        }

        private UpdateDefinition<StockFinancials> AddEarningsPerShareUpdate(List<EarningsPerShare> earningsPerShares)
        {
            return Builders<StockFinancials>.Update.PushEach("EarningsPerShare", earningsPerShares);
        }

        private UpdateDefinition<StockFinancials> AddSalesPerShareUpdate(List<SalesPerShare> salesPerShares)
        {
            return Builders<StockFinancials>.Update.PushEach("SalesPerShare", salesPerShares);
        }

        private UpdateDefinition<StockFinancials> AddBookValuePerShareUpdate(List<BookValuePerShare> bookValues)
        {
            return Builders<StockFinancials>.Update.PushEach("BookValuePerShare", bookValues);
        }

        private UpdateDefinition<StockFinancials> AddPayoutRatioUpdate(List<PayoutRatio> payoutRatios)
        {
            return Builders<StockFinancials>.Update.PushEach("PayoutRatio", payoutRatios);
        }

        private UpdateDefinition<StockFinancials> AddCurrentRatioUpdate(List<CurrentRatio> currentRatios)
        {
            return Builders<StockFinancials>.Update.PushEach("CurrentRatio", currentRatios);
        }

        private UpdateDefinition<StockFinancials> AddDebtToEquityRatioUpdate(List<DebtToEquityRatio> debtToEquityRatios)
        {
            return Builders<StockFinancials>.Update.PushEach("DebtToEquityRatio", debtToEquityRatios);
        }

        private UpdateDefinition<StockFinancials> AddEnterpriseValueUpdate(List<EnterpriseValue> enterpriseValues)
        {
            return Builders<StockFinancials>.Update.PushEach("EnterpriseValue", enterpriseValues);
        }

        private UpdateDefinition<StockFinancials> AddEBITDAUpdate(List<EBITDA> eBITDAs)
        {
            return Builders<StockFinancials>.Update.PushEach("EBITDA", eBITDAs);
        }

        private UpdateDefinition<StockFinancials> AddFreeCashFlowUpdate(List<FreeCashFlow> freeCashFlows)
        {
            return Builders<StockFinancials>.Update.PushEach("FreeCashFlow", freeCashFlows);
        }

        private UpdateDefinition<StockFinancials> AddGrossMarginUpdate(List<GrossMargin> grossMargins)
        {
            return Builders<StockFinancials>.Update.PushEach("GrossMargin", grossMargins);
        }

        private UpdateDefinition<StockFinancials> AddProfitMarginUpdate(List<ProfitMargin> profitMargins)
        {
            return Builders<StockFinancials>.Update.PushEach("ProfitMargin", profitMargins);
        }

        private UpdateDefinition<StockFinancials> AddRevenuesUpdate(List<Revenues> revenues)
        {
            return Builders<StockFinancials>.Update.PushEach("Revenues", revenues);
        }

        private UpdateDefinition<StockFinancials> AddWorkingCapitalUpdate(List<WorkingCapital> workingCapitals)
        {
            return Builders<StockFinancials>.Update.PushEach("WorkingCapital", workingCapitals);
        }
    }
}