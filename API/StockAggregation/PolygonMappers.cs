using ApiClient.Models;
using Core;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation
{
	internal static class PolygonMappers 
	{
		public static IEnumerable<MarketCap> GetNewMarketCap(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.MarketCap).Select(x => new MarketCap()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				marketCap = x.MarketCapitalization
			});
		}

		public static IEnumerable<DividendsPerShare> GetNewDividendsPershare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.DividendsPerShare).Select(x => new DividendsPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				dividendsPerShare = x.DividendsPerBasicCommonShare
			});
		}

		public static IEnumerable<EarningsPerShare> GetNewEarningsPerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.EarningsPerShare).Select(x => new EarningsPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				earningsPerShare = x.EarningsPerBasicShare
			});
		}

		public static IEnumerable<SalesPerShare> GetNewSalesPerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.SalesPerShare).Select(x => new SalesPerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				salesPerShare = x.SalesPerShare
			});
		}

		public static IEnumerable<BookValuePerShare> GetNewBookValuePerShare(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.BookValuePerShare).Select(x => new BookValuePerShare()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				bookValuePerShare = x.BookValuePerShare
			});
		}

		public static IEnumerable<PayoutRatio> GetNewPayoutRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.PayoutRatio).Select(x => new PayoutRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				payoutRatio = x.PayoutRatio
			});
		}

		public static IEnumerable<CurrentRatio> GetNewCurrentRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.CurrentRatio).Select(x => new CurrentRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				currentRatio = x.CurrentRatio
			});
		}

		public static IEnumerable<DebtToEquityRatio> GetNewDebtToEquityRatio(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.DebtToEquityRatio).Select(x => new DebtToEquityRatio()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				debtToEquityRatio = x.DebtToEquityRatio
			});
		}

		public static IEnumerable<EnterpriseValue> GetNewEnterpriseValue(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.EnterpriseValue).Select(x => new EnterpriseValue()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				enterpriseValue = x.EnterpriseValue
			});
		}

		public static IEnumerable<EBITDA> GetNewEBITDA(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.EBITDA).Select(x => new EBITDA()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				ebitda = x.EarningsBeforeInterestTaxesDepreciationAmortization
			});
		}

		public static IEnumerable<FreeCashFlow> GetNewFreeCashFlow(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.FreeCashFlow).Select(x => new FreeCashFlow()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				freeCashFlow = x.FreeCashFlow
			});
		}

		public static IEnumerable<GrossMargin> GetNewGrossMargin(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.GrossMargin).Select(x => new GrossMargin()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				grossMargin = x.GrossMargin
			});
		}

		public static IEnumerable<ProfitMargin> GetNewProfitMargin(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.ProfitMargin).Select(x => new ProfitMargin()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				profitMargin = x.ProfitMargin
			});
		}

		public static IEnumerable<Revenues> GetNewRevenues(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.Revenues).Select(x => new Revenues()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				revenues = x.Revenues
			});
		}

		public static IEnumerable<WorkingCapital> GetNewWorkingCapital(StockFinancials dbEntry, PolygonStockFinancialsResponse response)
		{
			return response.GetNewStockFinancialsEntries(dbEntry?.WorkingCapital).Select(x => new WorkingCapital()
			{
				timestamp = x.ReportPeriod.ToUnix(),
				workingCapital = x.WorkingCapital
			});
		}

		public static TPriceData MapToPriceData<TPriceData>(PolygonPriceDataResponse response, double timestamp = 0) where TPriceData : PriceData
		{
			var priceData = Activator.CreateInstance<TPriceData>();
			priceData.Ticker = response.Ticker;

			foreach (var minuteData in response?.Results)
			{
				if (minuteData.T > timestamp)
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
	}
}
