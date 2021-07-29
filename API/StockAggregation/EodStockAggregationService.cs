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
		private readonly CashFlowHistoryRepository cashFlowHistoryRepository;
		private readonly IncomeStatementHistoryRepository incomeStatementHistoryRepository;
		private readonly BalanceSheetHistoryRepository balanceSheetHistoryRepository;
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
			cashFlowHistoryRepository = new CashFlowHistoryRepository(stockDataContext);
			balanceSheetHistoryRepository = new BalanceSheetHistoryRepository(stockDataContext);
			incomeStatementHistoryRepository = new IncomeStatementHistoryRepository(stockDataContext);
			this.logger = logger;
		}

		public void LoadTickersByIndex(string index)
		{
			var result = eodClient.GetIndexInfo(index);

			stockIndexRepository.CreateEntryForIndex(index, result.Components.Select(x => x.Value.Code).ToList());
		}

		public void LoadDailyPriceDataForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach(var ticker in tickers)
			{
				var result = eodClient.GetPriceData(ticker);

				if(result is not null)
					priceDataRepository.Update(EodMappers.MapToPriceData<DayPriceData>(ticker, result));
			}
		}

		public void LoadCompanyInfoForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach(var ticker in tickers)
			{
				var result = eodClient.GetCompanyInfo(ticker);

				if(result is not null)
					companyInfoRepository.Update(EodMappers.MapCompanyInfo(result));
			}
		}

		public void LoadEarningsForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

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

		public void LoadOutstandingSharesForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetOutstandingShares(ticker);

				if (result is not null)
					outstandingSharesRepository.Update(EodMappers.MapOutstandingShares(result));
			}
		}

		public void LoadBalanceSheetForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetBalanceSheet(ticker);

				result.quarterly.RemoveAll((key, value) => value.filing_date is null);

				if (result is not null)
					balanceSheetHistoryRepository.Update(EodMappers.MapBalanceSheet(result));
			}
		}

		public void LoadIncomeStatementForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetIncomeStatement(ticker);

				result.quarterly.RemoveAll((key, value) => value.filing_date is null);

				if (result is not null)
					incomeStatementHistoryRepository.Update(EodMappers.MapIncomeStatement(result));
			}
		}

		public void LoadCashFlowForExchange(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;

			foreach (var ticker in tickers)
			{
				var result = eodClient.GetCashFlow(ticker);

				result.quarterly.RemoveAll((key, value) => value.filing_date is null);

				if (result is not null)
					cashFlowHistoryRepository.Update(EodMappers.MapCashFlow(result));
			}
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

		public void LoadHourlyPriceDataForExchange(string index)
		{
			throw new NotImplementedException();
		}
	}
}
