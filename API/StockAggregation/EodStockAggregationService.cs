using ApiClient;
using ApiClient.Models.Eod;
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
		private readonly QuarterPriceDataRepository monthPriceDataRepository;
		private readonly CompanyInfoRepository companyInfoRepository;
		private readonly StockFinancialsRepository stockFinancialsRepository;
		private readonly OutstandingSharesHistoryRepository outstandingSharesRepository;
		private readonly CashFlowHistoryRepository cashFlowHistoryRepository;
		private readonly IncomeStatementHistoryRepository incomeStatementHistoryRepository;
		private readonly BalanceSheetHistoryRepository balanceSheetHistoryRepository;
		private readonly StockIndexRepository stockIndexRepository;
		private readonly YearEarningsDataRepository earningsRepository;
		private readonly IEodClient eodClient;
		private readonly ILogger logger;

		public EodStockAggregationService(IMongoDBContext stockDataContext, IMongoDBContext priceDataContext, IEodClient client, ILogger logger)
		{
			eodClient = client;
			monthPriceDataRepository = new QuarterPriceDataRepository(priceDataContext);
			companyInfoRepository = new CompanyInfoRepository(stockDataContext);
			stockFinancialsRepository = new StockFinancialsRepository(stockDataContext);
			stockIndexRepository = new StockIndexRepository(stockDataContext);
			earningsRepository = new YearEarningsDataRepository(stockDataContext);
			outstandingSharesRepository = new OutstandingSharesHistoryRepository(stockDataContext);
			cashFlowHistoryRepository = new CashFlowHistoryRepository(stockDataContext);
			balanceSheetHistoryRepository = new BalanceSheetHistoryRepository(stockDataContext);
			incomeStatementHistoryRepository = new IncomeStatementHistoryRepository(stockDataContext);
			this.logger = logger;
		}

		public void LoadStockData(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;
			var count = 0;

			foreach (var ticker in tickers)
			{
				var eodFundementals = eodClient.GetFundementals(ticker);
				var priceData = EodMappers.MapQuarterPriceData(ticker, eodClient.GetPriceData(ticker));
				
				monthPriceDataRepository.LoadPriceData(priceData);
				WriteEarnings(eodFundementals);
				Console.WriteLine($"{ticker} - {count++}");
				var blah = 2;

			}
		}

		private void WriteEarnings(EodFundementals eodFundementals)
		{
			var dbEarnings = EodMappers.MapEarnings(eodFundementals.Ticker, eodFundementals.Earnings, eodFundementals.Financials.Income_Statement);
			earningsRepository.Load(dbEarnings);
		} 

		public void LoadTickersByIndex(string index)
		{
			var result = eodClient.GetIndexInfo(index);

			stockIndexRepository.CreateEntryForIndex(index, result.Components.Select(x => x.Value.Code).ToList());
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

		public void LoadHourlyPriceDataForIndex(string index)
		{
			throw new NotImplementedException();
		}

		public void LoadDerivedValuesForIndex(string index)
		{
			throw new NotImplementedException();
		}

		public void UpdateCompanyInfoForMarket(string market)
		{
			throw new NotImplementedException();
		}

		public void LoadDailyPriceDataForIndex(string index)
		{
			throw new NotImplementedException();
		}

		public void LoadStockFundementalData(string index)
		{
			throw new NotImplementedException();
		}
	}
}
