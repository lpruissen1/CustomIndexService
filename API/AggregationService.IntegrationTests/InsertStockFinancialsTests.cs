using ApiClient.Models;
using Core;
using NUnit.Framework;
using System;

namespace AggregationService.IntegrationTests
{
    [TestFixture]
    public class InsertStockFinancialsTest : AggregationServiceTestBase
    {
		private StockAggregationService sut;

        [Test]
        public void InsertStockFinancials()
        {
			var ticker = "EXMP";

			var marketCap1 = 12_345_678d;
			var dividendsPerShare1 = 0.45d;
			var eps1 = 1.2325d;
			var salesPerShare1 = 4.32d;
			var bookValuePerShare1 = 5.5d;
			var payoutRatio1 = 0.10d;
			var currentRatio1 = 10.1d;
			var debtToEquityRatio1 = 2.4d;
			var enterpriseValue1 = 123_456_789d;
			var ebitda1 = 123_456d;
			var freeCashFlow1 = 123d;
			var grossMargin1 = 0.4d;
			var profitMargin1 = 0.05d;
			var revenues1 = 111_111_111d;
			var workingCapital1 = 222_222_222d; 
			var reportPeriod1 = new DateTime(2001, 1, 1);

			var marketCap2 = 12_345_111d;
			var dividendsPerShare2 = 0.4d;
			var eps2 = 1.238d;
			var salesPerShare2 = 4.67d;
			var bookValuePerShare2 = 5.9d;
			var payoutRatio2 = 0.11d;
			var currentRatio2 = 10.8d;
			var debtToEquityRatio2 = 2.1d;
			var enterpriseValue2 = 123_456_111d;
			var ebitda2 = 123_789d;
			var freeCashFlow2 = 128d;
			var grossMargin2 = 0.2d;
			var profitMargin2 = 0.12d;
			var revenues2 = 111_111_222d;
			var workingCapital2 = 222_222_333d;
			var reportPeriod2 = new DateTime(2001, 4, 1);


			var stubResponse = new PolygonStockFinancialsResponse()
			{
				Results = new[]
				{
					new QuarterlyStockFinancialsData
					{
						Ticker = ticker,
						MarketCapitalization = marketCap1,
						DividendsPerBasicCommonShare = dividendsPerShare1,
						EarningsPerBasicShare = eps1,
						SalesPerShare = salesPerShare1,
						BookValuePerShare = bookValuePerShare1,
						PayoutRatio = payoutRatio1,
						CurrentRatio = currentRatio1,
						DebtToEquityRatio = debtToEquityRatio1,
						EnterpriseValue = enterpriseValue1,
						EarningsBeforeInterestTaxesDepreciationAmortization = ebitda1,
						FreeCashFlow = freeCashFlow1,
						GrossMargin = grossMargin1,
						ProfitMargin = profitMargin1,
						Revenues = revenues1,
						WorkingCapital = workingCapital1,
						ReportPeriod = reportPeriod1
					},

					new QuarterlyStockFinancialsData
					{
						Ticker = ticker,
						MarketCapitalization = marketCap2,
						DividendsPerBasicCommonShare = dividendsPerShare2,
						EarningsPerBasicShare = eps2,
						SalesPerShare = salesPerShare2,
						BookValuePerShare = bookValuePerShare2,
						PayoutRatio = payoutRatio2,
						CurrentRatio = currentRatio2,
						DebtToEquityRatio = debtToEquityRatio2,
						EnterpriseValue = enterpriseValue2,
						EarningsBeforeInterestTaxesDepreciationAmortization = ebitda2,
						FreeCashFlow = freeCashFlow2,
						GrossMargin = grossMargin2,
						ProfitMargin = profitMargin2,
						Revenues = revenues2,
						WorkingCapital = workingCapital2,
						ReportPeriod = reportPeriod2
					}
				}
			};

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.InsertStockFinancials(ticker);

			var result = GetStockFinancials(ticker);

			Assert.AreEqual(marketCap1, result.MarketCap[0].marketCap);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);
			Assert.AreEqual(marketCap2, result.MarketCap[1].marketCap);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.MarketCap[1].timestamp);

			Assert.AreEqual(dividendsPerShare1, result.DividendsPerShare[0].dividendsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DividendsPerShare[0].timestamp);
			Assert.AreEqual(dividendsPerShare2, result.DividendsPerShare[1].dividendsPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.DividendsPerShare[1].timestamp);

			Assert.AreEqual(eps1, result.EarningsPerShare[0].earningsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);
			Assert.AreEqual(eps2, result.EarningsPerShare[1].earningsPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.MarketCap[1].timestamp);

			Assert.AreEqual(salesPerShare1, result.SalesPerShare[0].salesPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.SalesPerShare[0].timestamp);
			Assert.AreEqual(salesPerShare2, result.SalesPerShare[1].salesPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.SalesPerShare[1].timestamp);

			Assert.AreEqual(bookValuePerShare1, result.BookValuePerShare[0].bookValuePerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.BookValuePerShare[0].timestamp);
			Assert.AreEqual(bookValuePerShare2, result.BookValuePerShare[1].bookValuePerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.BookValuePerShare[1].timestamp);

			Assert.AreEqual(payoutRatio1, result.PayoutRatio[0].payoutRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.PayoutRatio[0].timestamp);
			Assert.AreEqual(payoutRatio2, result.PayoutRatio[1].payoutRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.PayoutRatio[1].timestamp);

			Assert.AreEqual(currentRatio1, result.CurrentRatio[0].currentRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.CurrentRatio[0].timestamp);
			Assert.AreEqual(currentRatio2, result.CurrentRatio[1].currentRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.CurrentRatio[1].timestamp);

			Assert.AreEqual(debtToEquityRatio1, result.DebtToEquityRatio[0].debtToEquityRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DebtToEquityRatio[0].timestamp);
			Assert.AreEqual(debtToEquityRatio2, result.DebtToEquityRatio[1].debtToEquityRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.DebtToEquityRatio[1].timestamp);

			Assert.AreEqual(enterpriseValue1, result.EnterpriseValue[0].enterpriseValue);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EnterpriseValue[0].timestamp);
			Assert.AreEqual(enterpriseValue2, result.EnterpriseValue[1].enterpriseValue);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.EnterpriseValue[1].timestamp);

			Assert.AreEqual(ebitda1, result.EBITDA[0].ebitda);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EBITDA[0].timestamp);
			Assert.AreEqual(ebitda2, result.EBITDA[1].ebitda);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.EBITDA[1].timestamp);

			Assert.AreEqual(freeCashFlow1, result.FreeCashFlow[0].freeCashFlow);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.FreeCashFlow[0].timestamp);
			Assert.AreEqual(freeCashFlow2, result.FreeCashFlow[1].freeCashFlow);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.FreeCashFlow[1].timestamp);

			Assert.AreEqual(grossMargin1, result.GrossMargin[0].grossMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.GrossMargin[0].timestamp);
			Assert.AreEqual(grossMargin2, result.GrossMargin[1].grossMargin);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.GrossMargin[1].timestamp);

			Assert.AreEqual(profitMargin1, result.ProfitMargin[0].profitMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.ProfitMargin[0].timestamp);
			Assert.AreEqual(profitMargin2, result.ProfitMargin[1].profitMargin);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.ProfitMargin[1].timestamp);

			Assert.AreEqual(revenues1, result.Revenues[0].revenues);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.Revenues[0].timestamp);
			Assert.AreEqual(revenues2, result.Revenues[1].revenues);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.Revenues[1].timestamp);

			Assert.AreEqual(workingCapital1, result.WorkingCapital[0].workingCapital);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.WorkingCapital[0].timestamp);
			Assert.AreEqual(workingCapital2, result.WorkingCapital[1].workingCapital);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.WorkingCapital[1].timestamp);
		}
	}

}