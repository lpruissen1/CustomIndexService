using ApiClient;
using Core;
using Database.Core;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using StockAggregation.Core;
using StockScreener.Database;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Repos;
using StockScreener.Database.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation
{
	public class StockAggregationService : IStockAggregationService
    {
		private IPolygonClient polygonApiClient;
		private readonly ILogger logger;
		private IStockFinancialsRepository stockFinancialsRepository;
		private IPriceDataRepository priceDataRepository;
		private ICompanyInfoRepository companyInfoRepository;
		private IStockIndexRepository stockIndexRepository;

        // IMongoIsInWeirdPlace
        public StockAggregationService(IMongoDBContext stockDataContext, IMongoDBContext priceDataContext, IPolygonClient client, ILogger logger)
        {
            polygonApiClient = client;
			this.logger = logger;
			priceDataRepository = new PriceDataRepository(priceDataContext);
            companyInfoRepository = new CompanyInfoRepository(stockDataContext);
            stockFinancialsRepository = new StockFinancialsRepository(stockDataContext);
            stockIndexRepository = new StockIndexRepository(stockDataContext);
        }

        public void UpdateCompanyInfoForMarket(string market)
        {
			foreach(var ticker in GetTickersByIndex(market)) {
				UpdateCompanyInfoForTicker(ticker);
			}
        }

        private void UpdateCompanyInfoForTicker(string ticker)
        {
            var response = polygonApiClient.GetCompanyInfo(ticker);

            var existingEntry = companyInfoRepository.Get(ticker);

            if (existingEntry is null || existingEntry.LastUpdated < response.Updated.ToUnix())
                companyInfoRepository.Update(new CompanyInfo
                {
                    Ticker = ticker,
                    Industry = response.Industry,
                    Sector = response.Sector,
                    Name = response.Name,
                    LastUpdated = response.Updated.ToUnix()
                });
        }

        public void UpdateStockFinancialsForMarket(string market)
		{
			foreach (var ticker in GetTickersByIndex(market))
			{
				UpdateStockFinancialsForTicker(ticker);
			}
		}

        private void UpdateStockFinancialsForTicker(string ticker)
        {
            var response = polygonApiClient.GetStockFinancials(ticker);

            var result = stockFinancialsRepository.Get(ticker);

            stockFinancialsRepository.Update(new StockFinancials
            {
                Ticker = ticker,
                MarketCap = PolygonMappers.GetNewMarketCap(result, response).ToList(),
                DividendsPerShare = PolygonMappers.GetNewDividendsPershare(result, response).ToList(),
                EarningsPerShare = PolygonMappers.GetNewEarningsPerShare(result, response).ToList(),
                SalesPerShare = PolygonMappers.GetNewSalesPerShare(result, response).ToList(),
                BookValuePerShare = PolygonMappers.GetNewBookValuePerShare(result, response).ToList(),
                PayoutRatio = PolygonMappers.GetNewPayoutRatio(result, response).ToList(),
                CurrentRatio = PolygonMappers.GetNewCurrentRatio(result, response).ToList(),
                DebtToEquityRatio = PolygonMappers.GetNewDebtToEquityRatio(result, response).ToList(),
                EnterpriseValue = PolygonMappers.GetNewEnterpriseValue(result, response).ToList(),
                EBITDA = PolygonMappers.GetNewEBITDA(result, response).ToList(),
                FreeCashFlow = PolygonMappers.GetNewFreeCashFlow(result, response).ToList(),
                GrossMargin = PolygonMappers.GetNewGrossMargin(result, response).ToList(),
                ProfitMargin = PolygonMappers.GetNewProfitMargin(result, response).ToList(),
                Revenues = PolygonMappers.GetNewRevenues(result, response).ToList(),
                WorkingCapital = PolygonMappers.GetNewWorkingCapital(result, response).ToList()
            });
        }

        public void UpdateDailyPriceDataForMarket(string market)
        {
			foreach (var ticker in GetTickersByIndex(market))
			{
				UpdateDailyPriceDataForTicker(ticker);
			}
		}

        private void UpdateDailyPriceDataForTicker(string ticker)
        {
            var mostRecentTimestamp = priceDataRepository.GetMostRecentPriceEntry<DayPriceData>(ticker);

            var response = polygonApiClient.GetPriceData(ticker, 1, TimePeriod.Day, mostRecentTimestamp, DateTime.UtcNow.ToUnix());

            if (response.Results is null)
                return;

            priceDataRepository.Update(PolygonMappers.MapToPriceData<DayPriceData>(response));
        }

        public void UpdateHourlyPriceDataForMarket(string market)
		{
			foreach (var ticker in GetTickersByIndex(market))
			{
				UpdateHourlyPriceDataForTicker(ticker);
			}
		}

        private void UpdateHourlyPriceDataForTicker(string ticker)
        {
			var mostRecentTimestamp = priceDataRepository.GetMostRecentPriceEntry<HourPriceData>(ticker);

			var response = polygonApiClient.GetPriceData(ticker, 1, TimePeriod.Hour, mostRecentTimestamp, DateTime.UtcNow.ToUnix());

            if (response.Results is null)
                return;

            priceDataRepository.Update(PolygonMappers.MapToPriceData<HourPriceData>(response, priceDataRepository.GetMostRecentPriceEntry<HourPriceData>(ticker)));
        }

		private IEnumerable<string> GetTickersByIndex(string market)
		{
			var result = stockIndexRepository.GetIndex(market)?.Tickers;

			if (result is not null)
				return result;

			logger.LogInformation(new EventId(1), $"Invalid market: {market}");

			return Enumerable.Empty<string>();
		}
    }
}
