using ApiClient.Models.Eod;
using Core;
using StockScreener.Database.Model;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.Price;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation
{
	internal static class EodMappers 
	{
		public static TPriceData MapToPriceData<TPriceData>(string ticker, List<EodCandle> response) where TPriceData : PriceData
		{
			var priceData = Activator.CreateInstance<TPriceData>();
			priceData.Ticker = ticker;

			foreach (var datapoint in response)
			{
				priceData.Candle.Add(new Candle
				{
					timestamp = datapoint.date.ToUnix(),
					openPrice = datapoint.open,
					closePrice = datapoint.close,
					highPrice = datapoint.high,
					lowPrice = datapoint.low
				});
			}

			return priceData;
		}

		public static CompanyInfo MapCompanyInfo(EodCompanyInfo response, string index)
		{
			return new CompanyInfo
			{
				Ticker = response.Ticker,
				Name = response.Name,
				Description = response.Description,
				Industry= response.Industry,
				Cusip = response.Cusip,
				isDelisted = response.isDelisted,
				Indices = new[] { index },
				LastUpdated = response.UpdatedAt.ToUnix()
			};
		}

		public static EarningsHistory MapEarnings(EodEarnings response)
		{
			return new EarningsHistory
			{
				Ticker = response.Ticker,
				Entries = response.History.Select(pair => new EarningsEntry
				{
					timestamp = pair.Key.ToUnix(),
					ReportDate = pair.Value.reportDate.ToUnix(),
					EaringsPerShare = pair.Value?.epsActual ?? 0
				}).ToList()
			};
		}

		public static OutstandingSharesHistory MapOutstandingShares(EodOutstandingShares response)
		{
			return new OutstandingSharesHistory
			{
				Ticker = response.Ticker,
				Entries = response.quarterly.Select(entry => new OutstandingSharesEntry
				{
					timestamp = entry.Value.dateFormatted.ToUnix(),
					OutstandingShares = entry.Value.shares
				}).ToList()
			};
		}

		public static CashFlowHistory MapCashFlow(EodCashFlow response)
		{
			return new CashFlowHistory
			{
				Ticker = response.Ticker,
				Entries = response.quarterly.Select(entry => new CashFlowEntry
				{
					timestamp = entry.Key.ToUnix(),
					DividendsPaid = entry.Value.dividendsPaid,
					FreeCashFlow = entry.Value.freeCashFlow
				}).ToList()
			};
		}

		public static IncomeStatementHistory MapIncomeStatement(EodIncomeStatement response)
		{
			return new IncomeStatementHistory
			{
				Ticker = response.Ticker,
				Entries = response.quarterly.Select(entry => new IncomeStatementEntry
				{
					timestamp = entry.Key.ToUnix(),
					Ebitda = entry.Value.ebitda,
					NetIncome = entry.Value.netIncome,
					RotalRevenue = entry.Value.totalRevenue
				}).ToList()
			};
		}

		public static BalanceSheetHistory MapBalanceSheet(EodBalanceSheet response)
		{
			return new BalanceSheetHistory
			{
				Ticker = response.Ticker,
				Entries = response.quarterly.Select(entry => new BalanceSheetEntry
				{
					timestamp = entry.Key.ToUnix(),
					TotalAssets = entry.Value.totalAssets,
					TotalLiababilites = entry.Value.totalLiab,
					CommonStockTotalEquity = entry.Value.commonStockTotalEquity,
					Cash = entry.Value.cash,
					NetWorkingCapital = entry.Value.netWorkingCapital,
				}).ToList()
			};
		}
	}
}
