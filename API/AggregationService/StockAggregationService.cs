using AggregationService;
using AggregationService.Core;
using ApiClient;
using ApiClient.Models;
using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using StockScreener.Database;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Repos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AggregationService
{
    public class StockAggregationService
    {
        protected IPolygonClient polygonApiClient;
		protected IStockFinancialsRepository stockFinancialsRepository;
		protected IPriceDataRepository priceDataRepository;
		protected ICompanyInfoRepository companyInfoRepository;
		protected IStockIndexRepository stockIndexRepository;

		// IMongoIsInWeirdPlace
		public StockAggregationService(IMongoDBContext stockDataContext, IMongoDBContext priceDataContext, IPolygonClient client)
        {
			polygonApiClient = client;
			priceDataRepository = new PriceDataRepository(priceDataContext);
            companyInfoRepository = new CompanyInfoRepository(stockDataContext);
			stockFinancialsRepository = new StockFinancialsRepository(stockDataContext);
			stockIndexRepository = new StockIndexRepository(stockDataContext);
		}

		public static StockAggregationService New()
		{
			var contextFactory = new MongoDbContextFactory();
			var apiSettingsFactory = new ApiSettingsFactory();
			return new StockAggregationService(contextFactory.GetStockContext(), contextFactory.GetPriceContext(), new PolygonClient(apiSettingsFactory.GetPolygonSettings()));
		}

		public IEnumerable<string> GetStockIndicesNames()
		{
			return stockIndexRepository.Get().Select(x => x.Name);
		}

		public IEnumerable<string> GetTickersByIndex(string name)
		{
			return stockIndexRepository.GetIndex(name).Tickers;

		}

		public void InsertCompanyInfo(string ticker)
        {
            var response = polygonApiClient.GetCompanyInfo(ticker);
			companyInfoRepository.Create(new CompanyInfo()
			{
				Sector = response.Sector,
				Industry = response.Industry,
				Ticker = ticker,
				Name = response.Name,
				LastUpdated = response.Updated.ToUnix()
			});
		}

		public void InsertHourlyPriceData(string ticker)
		{
			var response = polygonApiClient.GetPriceData(ticker, 1, TimeResolution.Hour);

			if (response.Results.Count != 0)
				priceDataRepository.Create(MapToPriceData<HourPriceData>(response));
		}
		public void InsertDailyPriceData(string ticker)
		{
			var response = polygonApiClient.GetPriceData(ticker, 1, TimeResolution.Day);

			if (response.Results.Count != 0)
				priceDataRepository.Create(MapToPriceData<DayPriceData>(response));
		}

		public void UpdateCompanyInfo(string ticker)
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

		public void InsertStockFinancials(string ticker)
		{
			var response = polygonApiClient.GetStockFinancials(ticker);

			if (response.Results.Length != 0)
				stockFinancialsRepository.Create(MapToStockFinancials(response));
		}

		public StockFinancials GetStockFinancials(string ticker)
		{
			return stockFinancialsRepository.Get(ticker);
		}

		public void UpdateStockFinancials(string ticker)
		{
			var response = polygonApiClient.GetStockFinancials(ticker);

			var result = stockFinancialsRepository.Get(ticker);

			stockFinancialsRepository.Update(new StockFinancials
			{
				Ticker = ticker,
				MarketCap = GetNewMarketCap(result, response).ToList(),
				DividendsPerShare = GetNewDividendsPershare(result, response).ToList(),
				EarningsPerShare = GetNewEarningsPerShare(result, response).ToList(),
				SalesPerShare = GetNewSalesPerShare(result, response).ToList(),
				BookValuePerShare = GetNewBookValuePerShare(result, response).ToList(),
				PayoutRatio = GetNewPayoutRatio(result, response).ToList(),
				CurrentRatio = GetNewCurrentRatio(result, response).ToList(),
				DebtToEquityRatio = GetNewDebtToEquityRatio(result, response).ToList(),
				EnterpriseValue = GetNewEnterpriseValue(result, response).ToList(),
				EBITDA = GetNewEBITDA(result, response).ToList(),
				FreeCashFlow = GetNewFreeCashFlow(result, response).ToList(),
				GrossMargin = GetNewGrossMargin(result, response).ToList(),
				ProfitMargin = GetNewProfitMargin(result, response).ToList(),
				Revenues = GetNewRevenues(result, response).ToList(),
				WorkingCapital = GetNewWorkingCapital(result, response).ToList()
			});
		}
		public void UpdateDailyPriceData(string ticker)
		{
			var response = polygonApiClient.GetPriceData(ticker, 1, TimeResolution.Day);

			if (response.Results is null)
				return;
			
			var result = priceDataRepository.Get(ticker);

			priceDataRepository.Update(MapToPriceData<DayPriceData>(response, priceDataRepository.GetMostRecentPriceEntry<DayPriceData>(ticker)));
		}

		public void UpdateHourlyPriceData(string ticker)
		{
			var response = polygonApiClient.GetPriceData(ticker, 1, TimeResolution.Hour);

			if(response.Results is null)
				return;

			var result = priceDataRepository.Get(ticker);
			priceDataRepository.Update(MapToPriceData<HourPriceData>(response, priceDataRepository.GetMostRecentPriceEntry<HourPriceData>(ticker)));
		}

		public void ClearStockFinancials()
		{
			stockFinancialsRepository.Clear("StockFinancials");
		}

		public void ClearPriceData()
		{
			priceDataRepository.Clear("HourlyPriceData");
			priceDataRepository.Clear("DailyPriceData");
		}

		public void ClearCompanyInfo()
		{
			companyInfoRepository.Clear("CompanyInfo");
		}

		private TPriceData MapToPriceData<TPriceData>(PolygonPriceDataResponse response, double timestamp = 0) where TPriceData : PriceData
		{
			var priceData = Activator.CreateInstance<TPriceData>();
			priceData.Ticker = response.Ticker;

			foreach (var minuteData in response.Results)
			{
				if ( minuteData.T > timestamp )
				{
					priceData.Candle.Add(new Candle
					{
						timestamp = minuteData.T,
						openPrice = minuteData.O,
						closePrice = minuteData.C,
						highPrice = minuteData.H,
						lowPrice = minuteData.L
					});
				}
			}

			return priceData;
		}

		private StockFinancials MapToStockFinancials(PolygonStockFinancialsResponse response)
		{
			var stockFinancials = new StockFinancials();
			stockFinancials.Ticker = response.Results[0].Ticker;

			foreach (var quarterData in response.Results)
			{
				stockFinancials.MarketCap.Add(new MarketCap { timestamp = quarterData.ReportPeriod.ToUnix(), marketCap = quarterData.MarketCapitalization });
				stockFinancials.DividendsPerShare.Add(new DividendsPerShare { timestamp = quarterData.ReportPeriod.ToUnix(), dividendsPerShare = quarterData.DividendsPerBasicCommonShare });
				stockFinancials.EarningsPerShare.Add(new EarningsPerShare { timestamp = quarterData.ReportPeriod.ToUnix(), earningsPerShare = quarterData.EarningsPerBasicShare });
				stockFinancials.SalesPerShare.Add(new SalesPerShare { timestamp = quarterData.ReportPeriod.ToUnix(), salesPerShare = quarterData.SalesPerShare });
				stockFinancials.BookValuePerShare.Add(new BookValuePerShare { timestamp = quarterData.ReportPeriod.ToUnix(), bookValuePerShare = quarterData.BookValuePerShare });
				stockFinancials.PayoutRatio.Add(new PayoutRatio { timestamp = quarterData.ReportPeriod.ToUnix(), payoutRatio = quarterData.PayoutRatio });
				stockFinancials.CurrentRatio.Add(new CurrentRatio { timestamp = quarterData.ReportPeriod.ToUnix(), currentRatio = quarterData.CurrentRatio });
				stockFinancials.DebtToEquityRatio.Add(new DebtToEquityRatio { timestamp = quarterData.ReportPeriod.ToUnix(), debtToEquityRatio = quarterData.DebtToEquityRatio });
				stockFinancials.EnterpriseValue.Add(new EnterpriseValue { timestamp = quarterData.ReportPeriod.ToUnix(), enterpriseValue = quarterData.EnterpriseValue });
				stockFinancials.EBITDA.Add(new EBITDA { timestamp = quarterData.ReportPeriod.ToUnix(), ebitda = quarterData.EarningsBeforeInterestTaxesDepreciationAmortization });
				stockFinancials.FreeCashFlow.Add(new FreeCashFlow { timestamp = quarterData.ReportPeriod.ToUnix(), freeCashFlow = quarterData.FreeCashFlow });
				stockFinancials.GrossMargin.Add(new GrossMargin { timestamp = quarterData.ReportPeriod.ToUnix(), grossMargin = quarterData.GrossMargin });
				stockFinancials.ProfitMargin.Add(new ProfitMargin { timestamp = quarterData.ReportPeriod.ToUnix(), profitMargin = quarterData.ProfitMargin });
				stockFinancials.Revenues.Add(new Revenues { timestamp = quarterData.ReportPeriod.ToUnix(), revenues = quarterData.Revenues });
				stockFinancials.WorkingCapital.Add(new WorkingCapital { timestamp = quarterData.ReportPeriod.ToUnix(), workingCapital = quarterData.WorkingCapital });
			}

			return stockFinancials;
		}

		private IEnumerable<MarketCap> GetNewMarketCap(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.MarketCap).Select(x => new MarketCap()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				marketCap = x.MarketCapitalization
			});
		}

		private IEnumerable<DividendsPerShare> GetNewDividendsPershare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.DividendsPerShare).Select(x => new DividendsPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				dividendsPerShare = x.DividendsPerBasicCommonShare
			});
		}

		private IEnumerable<EarningsPerShare> GetNewEarningsPerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.EarningsPerShare).Select(x => new EarningsPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				earningsPerShare = x.EarningsPerBasicShare
			});
		}

		private IEnumerable<SalesPerShare> GetNewSalesPerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.SalesPerShare).Select(x => new SalesPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				salesPerShare = x.SalesPerShare
			});
		}

		private IEnumerable<BookValuePerShare> GetNewBookValuePerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.BookValuePerShare).Select(x => new BookValuePerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				bookValuePerShare = x.BookValuePerShare
			});
		}

		private IEnumerable<PayoutRatio> GetNewPayoutRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.PayoutRatio).Select(x => new PayoutRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				payoutRatio = x.PayoutRatio
			});
		}

		private IEnumerable<CurrentRatio> GetNewCurrentRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.CurrentRatio).Select(x => new CurrentRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				currentRatio = x.CurrentRatio
			});
		}

		private IEnumerable<DebtToEquityRatio> GetNewDebtToEquityRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.DebtToEquityRatio).Select(x => new DebtToEquityRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				debtToEquityRatio = x.DebtToEquityRatio
			});
		}

		private IEnumerable<EnterpriseValue> GetNewEnterpriseValue(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.EnterpriseValue).Select(x => new EnterpriseValue()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				enterpriseValue = x.EnterpriseValue
			});
		}

		private IEnumerable<EBITDA> GetNewEBITDA(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.EBITDA).Select(x => new EBITDA()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				ebitda = x.EarningsBeforeInterestTaxesDepreciationAmortization
			});
		}

		private IEnumerable<FreeCashFlow> GetNewFreeCashFlow(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.FreeCashFlow).Select(x => new FreeCashFlow()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				freeCashFlow = x.FreeCashFlow
			});
		}

		private IEnumerable<GrossMargin> GetNewGrossMargin(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.GrossMargin).Select(x => new GrossMargin()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				grossMargin = x.GrossMargin
			});
		}

		private IEnumerable<ProfitMargin> GetNewProfitMargin(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.ProfitMargin).Select(x => new ProfitMargin()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				profitMargin = x.ProfitMargin
			});
		}

		private IEnumerable<Revenues> GetNewRevenues(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.Revenues).Select(x => new Revenues()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				revenues = x.Revenues
			});
		}

		private IEnumerable<WorkingCapital> GetNewWorkingCapital(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry.WorkingCapital).Select(x => new WorkingCapital()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				workingCapital = x.WorkingCapital
			});
		}
	}
}
