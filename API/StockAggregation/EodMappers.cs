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

		public static IEnumerable<QuarterPriceData> MapQuarterPriceData(string ticker, List<EodCandle> response)
		{
			var list = new List<QuarterPriceData>();

			if (response is null)
				return list;

			var monthEntry = new QuarterPriceData { Ticker = ticker, Month = new DateTime(response[0].date.Year, response[0].date.Month, 1) };
			list.Add(monthEntry);

			foreach (var datapoint in response)
			{
				if (!monthEntry.Month.SameQuarter(datapoint.date))
				{
					monthEntry = new QuarterPriceData { Ticker = ticker, Month = new DateTime(datapoint.date.Year, datapoint.date.Month, 1) };
					list.Add(monthEntry);
				}

				monthEntry.Days.Add(new Candle
				{
					timestamp = datapoint.date.ToUnix(),
					openPrice = datapoint.open,
					closePrice = datapoint.close,
					highPrice = datapoint.high,
					lowPrice = datapoint.low
				});
			}

			return list;
		}

		public static CompanyInfo MapCompanyInfo(EodFundementals response, string index)
		{
			return new CompanyInfo
			{
				Ticker = response.Ticker,
				Name = response.General.Name,
				Description = response.General.Description,
				Industry= response.General.Industry,
				Sector= response.General.Sector,
				Cusip = response.General.Cusip,
				isDelisted = response.General.isDelisted,
				Indices = new[] { index },
				LastUpdated = response.General.UpdatedAt.ToUnix()
			};
		}

		public static IEnumerable<YearEarningsData> MapEarnings(string ticker, EodEarnings response, EodIncomeStatement incomeStatement)
		{
			var list = new List<YearEarningsData>();

			if (response is null)
				return list;

			response.History.RemoveAll((key, value) => value.epsActual is null);

			var yearEntry = new YearEarningsData { Ticker = ticker, Year = new DateTime(response.History.First().Key.Year, 1, 1) };
			list.Add(yearEntry);

			foreach (var datapoint in response.History)
			{
				if (yearEntry.Year.Year != datapoint.Key.Year)
				{
					yearEntry = new YearEarningsData { Ticker = ticker, Year = new DateTime(datapoint.Key.Year, 1, 1) };
					list.Add(yearEntry);
				}

				yearEntry.Quarters.Add(new EarningsEntry
				{
					timestamp = datapoint.Key.ToUnix(),
					ReportDate = datapoint.Value.reportDate,
					EaringsPerShare = datapoint.Value.epsActual.GetValueOrDefault(),
					EBITA = incomeStatement.quarterly.GetValueOrDefault(datapoint.Key)?.ebitda.GetValueOrDefault() ?? 0
				});
			}

			return list;
		}

		public static OutstandingSharesHistory MapOutstandingShares(EodOutstandingShares response)
		{
			return new OutstandingSharesHistory
			{
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
				Entries = response.quarterly.Select(entry => new IncomeStatementEntry
				{
					timestamp = entry.Key.ToUnix(),
					Ebitda = entry.Value.ebitda,
					NetIncome = entry.Value.netIncome,
					TotalRevenue = entry.Value.totalRevenue
				}).ToList()
			};
		}

		public static BalanceSheetHistory MapBalanceSheet(EodBalanceSheet response)
		{
			return new BalanceSheetHistory
			{
				Entries = response.quarterly.Select(entry => new BalanceSheetEntry
				{
					timestamp = entry.Key.ToUnix(),
					TotalAssets = entry.Value.totalAssets,
					TotalLiabilites = entry.Value.totalLiab,
					CommonStockTotalEquity = entry.Value.commonStockTotalEquity,
					Cash = entry.Value.cash,
					NetWorkingCapital = entry.Value.netWorkingCapital,
				}).ToList()
			};
		}
	}
}
