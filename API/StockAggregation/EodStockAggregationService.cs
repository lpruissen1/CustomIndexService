using ApiClient;
using ApiClient.Models.Eod;
using Database.Core;
using Microsoft.Extensions.Logging;
using StockAggregation.Core;
using StockScreener.Database.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation
{
	public class EodStockAggregationService : IStockAggregationService, IStockAggregationLoaderService
	{
		private readonly QuarterPriceDataRepository monthPriceDataRepository;
		private readonly CompanyInfoRepository companyInfoRepository;
		private readonly StockFinancialsRepository stockFinancialsRepository;
		private readonly OutstandingSharesHistoryRepository outstandingSharesRepository;
		private readonly YearCashFlowDataRepository cashFlowHistoryRepository;
		private readonly YearDividendDataRepository yearDividendDataRepository;
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
			cashFlowHistoryRepository = new YearCashFlowDataRepository(stockDataContext);
			balanceSheetHistoryRepository = new BalanceSheetHistoryRepository(stockDataContext);
			yearDividendDataRepository = new YearDividendDataRepository(stockDataContext);
			this.logger = logger;
		}

		public void LoadStockData(string index)
		{
			var tickers = stockIndexRepository.GetIndex(index).Tickers;
			var count = 0;

			foreach (var ticker in tickers)
			{
				var eodFundementals = eodClient.GetFundementals(ticker);
				var eodDividendData = eodClient.GetDividendData(ticker);
				var priceData = EodMappers.MapQuarterPriceData(ticker, eodClient.GetPriceData(ticker));
				
				monthPriceDataRepository.LoadPriceData(priceData);
				WriteEarnings(eodFundementals);
				WriteCompanyInfo(eodFundementals, index);
				WriteDividendData(eodFundementals, eodDividendData);
				Console.WriteLine($"{ticker} - {count++}");
				var blah = 2;
			}
		}

		private void WriteCompanyInfo(EodFundementals eodFundementals, string index)
		{
			var companyInfo = EodMappers.MapCompanyInfo(eodFundementals, index);
			companyInfoRepository.Create(companyInfo);
		} 

		private void WriteDividendData(EodFundementals eodFundementals, List<EodDividend> eodDividends)
		{
			var dividendData = EodMappers.MapDividendData(eodFundementals.Ticker, eodFundementals.Earnings, eodDividends);
			yearDividendDataRepository.Load(dividendData);
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
