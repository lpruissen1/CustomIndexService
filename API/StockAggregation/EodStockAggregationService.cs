using ApiClient;
using Core;
using Database.Core;
using Microsoft.Extensions.Logging;
using StockAggregation.Core;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Repos;
using System;
using System.Linq;

namespace StockAggregation
{
	public class EodStockAggregationService : IStockAggregationService, IStockAggregationLoaderService
	{
		private readonly PriceDataRepository priceDataRepository;
		private readonly CompanyInfoRepository companyInfoRepository;
		private readonly StockFinancialsRepository stockFinancialsRepository;
		private readonly OutstandingSharesHistoryRepository outstandingSharesRepository;
		private readonly StockIndexRepository stockIndexRepository;
		private readonly EarningsRepository earningsRepository;
		private readonly IEodClient eodClient;
		private readonly ILogger logger;

		public EodStockAggregationService(IMongoDBContext stockDataContext, IMongoDBContext priceDataContext, IEodClient client, ILogger logger)
		{
			eodClient = client;
			priceDataRepository = new PriceDataRepository(priceDataContext);
			companyInfoRepository = new CompanyInfoRepository(stockDataContext);
			stockFinancialsRepository = new StockFinancialsRepository(stockDataContext);
			stockIndexRepository = new StockIndexRepository(stockDataContext);
			earningsRepository = new EarningsRepository(stockDataContext);
			outstandingSharesRepository = new OutstandingSharesHistoryRepository(stockDataContext);
			this.logger = logger;
		}

		public void LoadTickersByExchange(string exchange)
		{
			var result = eodClient.GetExhangeInfo(exchange).Where(x => x.Type == "Common Stock");

			stockIndexRepository.CreateEntryForExchange(exchange, result.Select(x => x.Code).ToList());
		}

		public void LoadDailyPriceDataForExchange(string exchange)
		{
			var tickers = stockIndexRepository.GetIndex(exchange).Tickers;

			foreach(var ticker in tickers)
			{
				var result = eodClient.GetPriceData(ticker);

				if(result is not null)
					priceDataRepository.Update(EodMappers.MapToPriceData<DayPriceData>(ticker, result));
			}
		}

		public void LoadCompanyInfoForExchange(string exchange)
		{
			var tickers = stockIndexRepository.GetIndex(exchange).Tickers;

			foreach(var ticker in tickers)
			{
				var result = eodClient.GetCompanyInfo(ticker);

				if(result is not null)
					companyInfoRepository.Update(EodMappers.MapCompanyInfo(result));
			}
		}

		public void LoadEarningsForExchange(string exchange)
		{
			var tickers = stockIndexRepository.GetIndex(exchange).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetEarnings(ticker);
				var now = DateTime.Now.ToUnix();

				// want to filter out those earnings who have not been reported
				result.History.RemoveAll((key, value) => value.reportDate.ToUnix() > now); 

				if (result is not null)
					earningsRepository.Update(EodMappers.MapEarnings(result));
			}
		}

		public void LoadOutstandingSharesForExchange(string exchange)
		{
			var tickers = stockIndexRepository.GetIndex(exchange).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetOutstandingShares(ticker);

				if (result is not null)
					outstandingSharesRepository.Update(EodMappers.MapOutstandingShares(result));
			}
		}

		public void LoadStockFinancialsForExchange(string exchange)
		{
			throw new NotImplementedException();
		}

		public void UpdateCompanyInfoForMarket(string market)
		{
			var tickers = stockIndexRepository.GetIndex(market).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetCompanyInfo(ticker);

				if (result is not null)
					companyInfoRepository.Update(EodMappers.MapCompanyInfo(result));
			}
		}

		public void UpdateDailyPriceDataForMarket(string market)
		{
			throw new NotImplementedException();
		}

		public void UpdateHourlyPriceDataForMarket(string market)
		{
			throw new NotImplementedException();
		}

		public void UpdateStockFinancialsForMarket(string market)
		{
			throw new NotImplementedException();
		}

		public void LoadHourlyPriceDataForExchange(string exchange)
		{
			throw new NotImplementedException();
		}
	}
}
