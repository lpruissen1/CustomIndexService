using ApiClient.Models;
using Core;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockAggregation.IntegrationTests
{
	[TestFixture]
	public class UpdateStockFinancialsTests : AggregationServiceTestBase
	{
		private StockAggregationService sut;

		[Test]
		public void UpdateStockFinancials_CreatesNewDatabaseEntry_WhenEntryIsLessThanResponse()
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
					},
				}
			};

			InsertStockFinancials(new StockFinancials
			{
				Ticker = ticker,
				MarketCap = new List<MarketCap>()
				{
					new MarketCap { marketCap = marketCap1, timestamp = reportPeriod1.ToUnix() }
				},
				DividendsPerShare = new List<DividendsPerShare>()
				{
					new DividendsPerShare { dividendsPerShare = dividendsPerShare1, timestamp = reportPeriod1.ToUnix()}
				},
				EarningsPerShare = new List<EarningsPerShare>()
				{
					new EarningsPerShare { earningsPerShare = eps1, timestamp = reportPeriod1.ToUnix()}
				},
				SalesPerShare = new List<SalesPerShare>()
				{
					new SalesPerShare { salesPerShare = salesPerShare1, timestamp = reportPeriod1.ToUnix()}
				},
				BookValuePerShare = new List<BookValuePerShare>()
				{
					new BookValuePerShare { bookValuePerShare = bookValuePerShare1, timestamp = reportPeriod1.ToUnix() }
				},
				PayoutRatio = new List<PayoutRatio>()
				{
					new PayoutRatio { payoutRatio = payoutRatio1, timestamp = reportPeriod1.ToUnix()}
				},
				CurrentRatio = new List<CurrentRatio>()
				{
					new CurrentRatio { currentRatio = currentRatio1, timestamp = reportPeriod1.ToUnix() }
				},
				DebtToEquityRatio = new List<DebtToEquityRatio>()
				{
					new DebtToEquityRatio { debtToEquityRatio = debtToEquityRatio1, timestamp = reportPeriod1.ToUnix()}
				},
				EnterpriseValue = new List<EnterpriseValue>()
				{
					new EnterpriseValue { enterpriseValue = enterpriseValue1, timestamp = reportPeriod1.ToUnix()}
				},
				EBITDA = new List<EBITDA>()
				{
					new EBITDA { ebitda = ebitda1, timestamp = reportPeriod1.ToUnix()}
				},
				FreeCashFlow = new List<FreeCashFlow>()
				{
					new FreeCashFlow { freeCashFlow = freeCashFlow1, timestamp = reportPeriod1.ToUnix()}
				},
				GrossMargin = new List<GrossMargin>()
				{
					new GrossMargin { grossMargin = grossMargin1, timestamp = reportPeriod1.ToUnix()}
				},
				ProfitMargin = new List<ProfitMargin>()
				{
					new ProfitMargin { profitMargin = profitMargin1, timestamp = reportPeriod1.ToUnix()}
				},
				Revenues = new List<Revenues>()
				{
					new Revenues { revenues = revenues1, timestamp = reportPeriod1.ToUnix()}
				},
				WorkingCapital = new List<WorkingCapital>()
				{
					new WorkingCapital { workingCapital = workingCapital1, timestamp = reportPeriod1.ToUnix()}
				}
			});

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateStockFinancialsForMarket(market);

			var result = GetStockFinancials(ticker);

			Assert.AreEqual(2, result.MarketCap.Count());
			Assert.AreEqual(2, result.DividendsPerShare.Count());
			Assert.AreEqual(2, result.EarningsPerShare.Count());
			Assert.AreEqual(2, result.SalesPerShare.Count());
			Assert.AreEqual(2, result.BookValuePerShare.Count());
			Assert.AreEqual(2, result.PayoutRatio.Count());
			Assert.AreEqual(2, result.CurrentRatio.Count());
			Assert.AreEqual(2, result.DebtToEquityRatio.Count());
			Assert.AreEqual(2, result.EnterpriseValue.Count());
			Assert.AreEqual(2, result.EBITDA.Count());
			Assert.AreEqual(2, result.FreeCashFlow.Count());
			Assert.AreEqual(2, result.GrossMargin.Count());
			Assert.AreEqual(2, result.ProfitMargin.Count());
			Assert.AreEqual(2, result.Revenues.Count());
			Assert.AreEqual(2, result.WorkingCapital.Count());

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

		[Test]
		public void UpdateStockFinancials_DoesNotCreateNewEntry_WhenCollectionIsUpToDate()
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
			var payoutRatio2 = 0.1d;
			var currentRatio2 = 10.8d;
			var debtToEquityRatio2 = 2.1d;
			var enterpriseValue2 = 123_456_111d;
			var ebitda2 = 123_789d;
			var freeCashFlow2 = 128d;
			var grossMargin2 = 0.2d;
			var profitMargin2 = 0.12d;
			var revenues2 = 111_111_222d;
			var workingCapital2 = 222_222_333d;
			var reportPeriod2 = new DateTime(2001, 1, 1);

			var stubResponse = new PolygonStockFinancialsResponse()
			{
				Results = new[]
				{
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

			InsertStockFinancials(new StockFinancials
			{
				Ticker = ticker,
				MarketCap = new List<MarketCap>()
				{
					new MarketCap { marketCap = marketCap1, timestamp = reportPeriod1.ToUnix() }
				},
				DividendsPerShare = new List<DividendsPerShare>()
				{
					new DividendsPerShare { dividendsPerShare = dividendsPerShare1, timestamp = reportPeriod1.ToUnix()}
				},
				EarningsPerShare = new List<EarningsPerShare>()
				{
					new EarningsPerShare { earningsPerShare = eps1, timestamp = reportPeriod1.ToUnix()}
				},
				SalesPerShare = new List<SalesPerShare>()
				{
					new SalesPerShare { salesPerShare = salesPerShare1, timestamp = reportPeriod1.ToUnix()}
				},
				BookValuePerShare = new List<BookValuePerShare>()
				{
					new BookValuePerShare { bookValuePerShare = bookValuePerShare1, timestamp = reportPeriod1.ToUnix() }
				},
				PayoutRatio = new List<PayoutRatio>()
				{
					new PayoutRatio { payoutRatio = payoutRatio1, timestamp = reportPeriod1.ToUnix()}
				},
				CurrentRatio = new List<CurrentRatio>()
				{
					new CurrentRatio { currentRatio = currentRatio1, timestamp = reportPeriod1.ToUnix() }
				},
				DebtToEquityRatio = new List<DebtToEquityRatio>()
				{
					new DebtToEquityRatio { debtToEquityRatio = debtToEquityRatio1, timestamp = reportPeriod1.ToUnix()}
				},
				EnterpriseValue = new List<EnterpriseValue>()
				{
					new EnterpriseValue { enterpriseValue = enterpriseValue1, timestamp = reportPeriod1.ToUnix()}
				},
				EBITDA = new List<EBITDA>()
				{
					new EBITDA { ebitda = ebitda1, timestamp = reportPeriod1.ToUnix()}
				},
				FreeCashFlow = new List<FreeCashFlow>()
				{
					new FreeCashFlow { freeCashFlow = freeCashFlow1, timestamp = reportPeriod1.ToUnix()}
				},
				GrossMargin = new List<GrossMargin>()
				{
					new GrossMargin { grossMargin = grossMargin1, timestamp = reportPeriod1.ToUnix()}
				},
				ProfitMargin = new List<ProfitMargin>()
				{
					new ProfitMargin { profitMargin = profitMargin1, timestamp = reportPeriod1.ToUnix()}
				},
				Revenues = new List<Revenues>()
				{
					new Revenues { revenues = revenues1, timestamp = reportPeriod1.ToUnix()}
				},
				WorkingCapital = new List<WorkingCapital>()
				{
					new WorkingCapital { workingCapital = workingCapital1, timestamp = reportPeriod1.ToUnix()}
				}
			});

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateStockFinancialsForMarket(market);

			var result = GetStockFinancials(ticker);

			Assert.AreEqual(1, result.MarketCap.Count());
			Assert.AreEqual(1, result.DividendsPerShare.Count());
			Assert.AreEqual(1, result.EarningsPerShare.Count());
			Assert.AreEqual(1, result.SalesPerShare.Count());
			Assert.AreEqual(1, result.BookValuePerShare.Count());
			Assert.AreEqual(1, result.PayoutRatio.Count());
			Assert.AreEqual(1, result.CurrentRatio.Count());
			Assert.AreEqual(1, result.DebtToEquityRatio.Count());
			Assert.AreEqual(1, result.EnterpriseValue.Count());
			Assert.AreEqual(1, result.EBITDA.Count());
			Assert.AreEqual(1, result.FreeCashFlow.Count());
			Assert.AreEqual(1, result.GrossMargin.Count());
			Assert.AreEqual(1, result.ProfitMargin.Count());
			Assert.AreEqual(1, result.Revenues.Count());
			Assert.AreEqual(1, result.WorkingCapital.Count());

			Assert.AreEqual(marketCap1, result.MarketCap[0].marketCap);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);

			Assert.AreEqual(dividendsPerShare1, result.DividendsPerShare[0].dividendsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DividendsPerShare[0].timestamp);

			Assert.AreEqual(eps1, result.EarningsPerShare[0].earningsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);

			Assert.AreEqual(salesPerShare1, result.SalesPerShare[0].salesPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.SalesPerShare[0].timestamp);

			Assert.AreEqual(bookValuePerShare1, result.BookValuePerShare[0].bookValuePerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.BookValuePerShare[0].timestamp);

			Assert.AreEqual(payoutRatio1, result.PayoutRatio[0].payoutRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.PayoutRatio[0].timestamp);

			Assert.AreEqual(currentRatio1, result.CurrentRatio[0].currentRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.CurrentRatio[0].timestamp);

			Assert.AreEqual(debtToEquityRatio1, result.DebtToEquityRatio[0].debtToEquityRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DebtToEquityRatio[0].timestamp);

			Assert.AreEqual(enterpriseValue1, result.EnterpriseValue[0].enterpriseValue);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EnterpriseValue[0].timestamp);

			Assert.AreEqual(ebitda1, result.EBITDA[0].ebitda);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EBITDA[0].timestamp);

			Assert.AreEqual(freeCashFlow1, result.FreeCashFlow[0].freeCashFlow);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.FreeCashFlow[0].timestamp);

			Assert.AreEqual(grossMargin1, result.GrossMargin[0].grossMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.GrossMargin[0].timestamp);

			Assert.AreEqual(profitMargin1, result.ProfitMargin[0].profitMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.ProfitMargin[0].timestamp);

			Assert.AreEqual(revenues1, result.Revenues[0].revenues);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.Revenues[0].timestamp);

			Assert.AreEqual(workingCapital1, result.WorkingCapital[0].workingCapital);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.WorkingCapital[0].timestamp);
		}


		[Test]
		public void UpdateStockFinancials_CreatesNewEntry_WhenNumberOfEntriesPerMetricIsDifferent()
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

			var marketCap3 = 12_345_762d;
			var dividendsPerShare3 = 0.123d;
			var eps3 = 1.38d;
			var salesPerShare3 = 4.7d;
			var bookValuePerShare3 = 5.43d;
			var payoutRatio3 = 0.106d;
			var currentRatio3 = 17.8d;
			var debtToEquityRatio3 = 2.98d;
			var enterpriseValue3 = 123_456_118d;
			var ebitda3 = 123_117d;
			var freeCashFlow3 = 113d;
			var grossMargin3 = 0.912d;
			var profitMargin3 = 0.1222d;
			var revenues3 = 111_111_546d;
			var workingCapital3 = 222_222_129d;
			var reportPeriod3 = new DateTime(2001, 7, 1);

			var stubResponse = new PolygonStockFinancialsResponse()
			{
				Results = new[]
				{
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
					},

					new QuarterlyStockFinancialsData
					{
						Ticker = ticker,
						MarketCapitalization = marketCap3,
						DividendsPerBasicCommonShare = dividendsPerShare3,
						EarningsPerBasicShare = eps3,
						SalesPerShare = salesPerShare3,
						BookValuePerShare = bookValuePerShare3,
						PayoutRatio = payoutRatio3,
						CurrentRatio = currentRatio3,
						DebtToEquityRatio = debtToEquityRatio3,
						EnterpriseValue = enterpriseValue3,
						EarningsBeforeInterestTaxesDepreciationAmortization = ebitda3,
						FreeCashFlow = freeCashFlow3,
						GrossMargin = grossMargin3,
						ProfitMargin = profitMargin3,
						Revenues = revenues3,
						WorkingCapital = workingCapital3,
						ReportPeriod = reportPeriod3
					}
				}
			};

			InsertStockFinancials(new StockFinancials
			{
				Ticker = ticker,
				MarketCap = new List<MarketCap>()
				{
					new MarketCap { marketCap = marketCap1, timestamp = reportPeriod1.ToUnix() }
				},
				DividendsPerShare = new List<DividendsPerShare>()
				{
					new DividendsPerShare { dividendsPerShare = dividendsPerShare1, timestamp = reportPeriod1.ToUnix()},
					new DividendsPerShare { dividendsPerShare = dividendsPerShare2, timestamp = reportPeriod2.ToUnix()}
				},
				EarningsPerShare = new List<EarningsPerShare>()
				{
					new EarningsPerShare { earningsPerShare = eps1, timestamp = reportPeriod1.ToUnix()},
					new EarningsPerShare { earningsPerShare = eps2, timestamp = reportPeriod2.ToUnix()},
					new EarningsPerShare { earningsPerShare = eps3, timestamp = reportPeriod3.ToUnix()}
				},
				SalesPerShare = new List<SalesPerShare>()
				{
					new SalesPerShare { salesPerShare = salesPerShare1, timestamp = reportPeriod1.ToUnix()}
				},
				BookValuePerShare = new List<BookValuePerShare>()
				{
					new BookValuePerShare { bookValuePerShare = bookValuePerShare1, timestamp = reportPeriod1.ToUnix() },
					new BookValuePerShare { bookValuePerShare = bookValuePerShare2, timestamp = reportPeriod2.ToUnix() }
				},
				PayoutRatio = new List<PayoutRatio>()
				{
					new PayoutRatio { payoutRatio = payoutRatio1, timestamp = reportPeriod1.ToUnix()},
					new PayoutRatio { payoutRatio = payoutRatio2, timestamp = reportPeriod2.ToUnix()},
					new PayoutRatio { payoutRatio = payoutRatio3, timestamp = reportPeriod3.ToUnix()}
				},
				CurrentRatio = new List<CurrentRatio>()
				{
					new CurrentRatio { currentRatio = currentRatio1, timestamp = reportPeriod1.ToUnix() }
				},
				DebtToEquityRatio = new List<DebtToEquityRatio>()
				{
					new DebtToEquityRatio { debtToEquityRatio = debtToEquityRatio1, timestamp = reportPeriod1.ToUnix()},
					new DebtToEquityRatio { debtToEquityRatio = debtToEquityRatio2, timestamp = reportPeriod2.ToUnix()}
				},
				EnterpriseValue = new List<EnterpriseValue>()
				{
					new EnterpriseValue { enterpriseValue = enterpriseValue1, timestamp = reportPeriod1.ToUnix()},
					new EnterpriseValue { enterpriseValue = enterpriseValue2, timestamp = reportPeriod2.ToUnix()},
					new EnterpriseValue { enterpriseValue = enterpriseValue3, timestamp = reportPeriod3.ToUnix()}
				},
				EBITDA = new List<EBITDA>()
				{
					new EBITDA { ebitda = ebitda1, timestamp = reportPeriod1.ToUnix()}
				},
				FreeCashFlow = new List<FreeCashFlow>()
				{
					new FreeCashFlow { freeCashFlow = freeCashFlow1, timestamp = reportPeriod1.ToUnix()},
					new FreeCashFlow { freeCashFlow = freeCashFlow2, timestamp = reportPeriod2.ToUnix()}
				},
				GrossMargin = new List<GrossMargin>()
				{
					new GrossMargin { grossMargin = grossMargin1, timestamp = reportPeriod1.ToUnix()},
					new GrossMargin { grossMargin = grossMargin2, timestamp = reportPeriod2.ToUnix()},
					new GrossMargin { grossMargin = grossMargin3, timestamp = reportPeriod3.ToUnix()}
				},
				ProfitMargin = new List<ProfitMargin>()
				{
					new ProfitMargin { profitMargin = profitMargin1, timestamp = reportPeriod1.ToUnix()}
				},
				Revenues = new List<Revenues>()
				{
					new Revenues { revenues = revenues1, timestamp = reportPeriod1.ToUnix()},
					new Revenues { revenues = revenues2, timestamp = reportPeriod2.ToUnix()}
				},
				WorkingCapital = new List<WorkingCapital>()
				{
					new WorkingCapital { workingCapital = workingCapital1, timestamp = reportPeriod1.ToUnix()},
					new WorkingCapital { workingCapital = workingCapital2, timestamp = reportPeriod2.ToUnix()},
					new WorkingCapital { workingCapital = workingCapital3, timestamp = reportPeriod3.ToUnix()}
				}
			});

			sut = new StockAggregationService(stockContext, priceContext, new FakePolygonClient(stubResponse));

			sut.UpdateStockFinancialsForMarket(market);

			var result = GetStockFinancials(ticker);

			Assert.AreEqual(3, result.MarketCap.Count());
			Assert.AreEqual(3, result.DividendsPerShare.Count());
			Assert.AreEqual(3, result.EarningsPerShare.Count());
			Assert.AreEqual(3, result.SalesPerShare.Count());
			Assert.AreEqual(3, result.BookValuePerShare.Count());
			Assert.AreEqual(3, result.PayoutRatio.Count());
			Assert.AreEqual(3, result.CurrentRatio.Count());
			Assert.AreEqual(3, result.DebtToEquityRatio.Count());
			Assert.AreEqual(3, result.EnterpriseValue.Count());
			Assert.AreEqual(3, result.EBITDA.Count());
			Assert.AreEqual(3, result.FreeCashFlow.Count());
			Assert.AreEqual(3, result.GrossMargin.Count());
			Assert.AreEqual(3, result.ProfitMargin.Count());
			Assert.AreEqual(3, result.Revenues.Count());
			Assert.AreEqual(3, result.WorkingCapital.Count());

			Assert.AreEqual(marketCap1, result.MarketCap[0].marketCap);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);
			Assert.AreEqual(marketCap2, result.MarketCap[1].marketCap);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.MarketCap[1].timestamp);
			Assert.AreEqual(marketCap3, result.MarketCap[2].marketCap);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.MarketCap[2].timestamp);

			Assert.AreEqual(dividendsPerShare1, result.DividendsPerShare[0].dividendsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DividendsPerShare[0].timestamp);
			Assert.AreEqual(dividendsPerShare2, result.DividendsPerShare[1].dividendsPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.DividendsPerShare[1].timestamp);
			Assert.AreEqual(dividendsPerShare3, result.DividendsPerShare[2].dividendsPerShare);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.DividendsPerShare[2].timestamp);

			Assert.AreEqual(eps1, result.EarningsPerShare[0].earningsPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.MarketCap[0].timestamp);
			Assert.AreEqual(eps2, result.EarningsPerShare[1].earningsPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.MarketCap[1].timestamp);
			Assert.AreEqual(eps3, result.EarningsPerShare[2].earningsPerShare);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.MarketCap[2].timestamp);

			Assert.AreEqual(salesPerShare1, result.SalesPerShare[0].salesPerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.SalesPerShare[0].timestamp);
			Assert.AreEqual(salesPerShare2, result.SalesPerShare[1].salesPerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.SalesPerShare[1].timestamp);
			Assert.AreEqual(salesPerShare3, result.SalesPerShare[2].salesPerShare);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.SalesPerShare[2].timestamp);

			Assert.AreEqual(bookValuePerShare1, result.BookValuePerShare[0].bookValuePerShare);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.BookValuePerShare[0].timestamp);
			Assert.AreEqual(bookValuePerShare2, result.BookValuePerShare[1].bookValuePerShare);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.BookValuePerShare[1].timestamp);
			Assert.AreEqual(bookValuePerShare3, result.BookValuePerShare[2].bookValuePerShare);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.BookValuePerShare[2].timestamp);

			Assert.AreEqual(payoutRatio1, result.PayoutRatio[0].payoutRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.PayoutRatio[0].timestamp);
			Assert.AreEqual(payoutRatio2, result.PayoutRatio[1].payoutRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.PayoutRatio[1].timestamp);
			Assert.AreEqual(payoutRatio3, result.PayoutRatio[2].payoutRatio);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.PayoutRatio[2].timestamp);

			Assert.AreEqual(currentRatio1, result.CurrentRatio[0].currentRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.CurrentRatio[0].timestamp);
			Assert.AreEqual(currentRatio2, result.CurrentRatio[1].currentRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.CurrentRatio[1].timestamp);
			Assert.AreEqual(currentRatio3, result.CurrentRatio[2].currentRatio);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.CurrentRatio[2].timestamp);

			Assert.AreEqual(debtToEquityRatio1, result.DebtToEquityRatio[0].debtToEquityRatio);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.DebtToEquityRatio[0].timestamp);
			Assert.AreEqual(debtToEquityRatio2, result.DebtToEquityRatio[1].debtToEquityRatio);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.DebtToEquityRatio[1].timestamp);
			Assert.AreEqual(debtToEquityRatio3, result.DebtToEquityRatio[2].debtToEquityRatio);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.DebtToEquityRatio[2].timestamp);

			Assert.AreEqual(enterpriseValue1, result.EnterpriseValue[0].enterpriseValue);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EnterpriseValue[0].timestamp);
			Assert.AreEqual(enterpriseValue2, result.EnterpriseValue[1].enterpriseValue);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.EnterpriseValue[1].timestamp);
			Assert.AreEqual(enterpriseValue3, result.EnterpriseValue[2].enterpriseValue);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.EnterpriseValue[2].timestamp);

			Assert.AreEqual(ebitda1, result.EBITDA[0].ebitda);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.EBITDA[0].timestamp);
			Assert.AreEqual(ebitda2, result.EBITDA[1].ebitda);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.EBITDA[1].timestamp);
			Assert.AreEqual(ebitda3, result.EBITDA[2].ebitda);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.EBITDA[2].timestamp);

			Assert.AreEqual(freeCashFlow1, result.FreeCashFlow[0].freeCashFlow);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.FreeCashFlow[0].timestamp);
			Assert.AreEqual(freeCashFlow2, result.FreeCashFlow[1].freeCashFlow);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.FreeCashFlow[1].timestamp);
			Assert.AreEqual(freeCashFlow3, result.FreeCashFlow[2].freeCashFlow);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.FreeCashFlow[2].timestamp);

			Assert.AreEqual(grossMargin1, result.GrossMargin[0].grossMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.GrossMargin[0].timestamp);
			Assert.AreEqual(grossMargin2, result.GrossMargin[1].grossMargin);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.GrossMargin[1].timestamp);
			Assert.AreEqual(grossMargin3, result.GrossMargin[2].grossMargin);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.GrossMargin[2].timestamp);

			Assert.AreEqual(profitMargin1, result.ProfitMargin[0].profitMargin);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.ProfitMargin[0].timestamp);
			Assert.AreEqual(profitMargin2, result.ProfitMargin[1].profitMargin);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.ProfitMargin[1].timestamp);
			Assert.AreEqual(profitMargin3, result.ProfitMargin[2].profitMargin);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.ProfitMargin[2].timestamp);

			Assert.AreEqual(revenues1, result.Revenues[0].revenues);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.Revenues[0].timestamp);
			Assert.AreEqual(revenues2, result.Revenues[1].revenues);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.Revenues[1].timestamp);
			Assert.AreEqual(revenues3, result.Revenues[2].revenues);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.Revenues[2].timestamp);

			Assert.AreEqual(workingCapital1, result.WorkingCapital[0].workingCapital);
			Assert.AreEqual(reportPeriod1.ToUnix(), result.WorkingCapital[0].timestamp);
			Assert.AreEqual(workingCapital2, result.WorkingCapital[1].workingCapital);
			Assert.AreEqual(reportPeriod2.ToUnix(), result.WorkingCapital[1].timestamp);
			Assert.AreEqual(workingCapital3, result.WorkingCapital[2].workingCapital);
			Assert.AreEqual(reportPeriod3.ToUnix(), result.WorkingCapital[2].timestamp);
		}
	}
}
