using ApiClient;
using Database.Core;
using Microsoft.Extensions.Logging;
using StockAggregation.Core;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Repos;
using System;
using System.Linq;

namespace StockAggregation
{
	public class EodStockAggregationService : IStockAggregationService
	{
		private readonly PriceDataRepository priceDataRepository;
		private readonly CompanyInfoRepository companyInfoRepository;
		private readonly StockFinancialsRepository stockFinancialsRepository;
		private readonly StockIndexRepository stockIndexRepository;
		private readonly IEodClient eodClient;
		private readonly ILogger logger;

		public EodStockAggregationService(IMongoDBContext stockDataContext, IMongoDBContext priceDataContext, IEodClient client, ILogger logger)
		{
			eodClient = client;
			priceDataRepository = new PriceDataRepository(priceDataContext);
			companyInfoRepository = new CompanyInfoRepository(stockDataContext);
			stockFinancialsRepository = new StockFinancialsRepository(stockDataContext);
			stockIndexRepository = new StockIndexRepository(stockDataContext);
			this.logger = logger;
		}

		public void LoadTickersByExchange(string exchange)
		{
			var result = eodClient.GetExhangeInfo(exchange).Where(x => x.Type == "Common Stock");

			stockIndexRepository.CreateEntryForExchange(exchange, result.Select(x => x.Code).ToList());
		}

		public void LoadPriceByExchange(string exchange)
		{
			var tickers = stockIndexRepository.GetIndex(exchange).Tickers;

			foreach(var ticker in tickers)
			{
				var result = eodClient.GetEodPriceData(ticker);

				if(result is not null)
					priceDataRepository.Update(EodMappers.MapToPriceData<DayPriceData>(ticker, result));
			}
		}

		public void UpdateCompanyInfoForMarket(string market)
		{
			throw new NotImplementedException();
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
	}
}
