using NUnit.Framework;
using StockScreener.Core;
using StockScreener.Service.IntegrationTests.StockDataHelpers;

namespace StockScreener.Service.IntegrationTests.Weighting
{
	[TestFixture]
	public class DividendYieldWeightingScreeningTest : WeightingTestBase
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			weightingRequest.Option = WeightingOption.DividendYield;
		}

		[Test]
		public void Weighting_DividendYieldWeight_NoManualSelection()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddTicker(ticker1);
			AddTicker(ticker2);

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(10));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.5, 1561867200)
				.AddDividendsPerShare(0.5d, 1569816000)
				.AddDividendsPerShare(0.5d, 1577768400)
				.AddDividendsPerShare(0.5, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(20));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 2);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(50m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(50m, result.Tickers[1].Weight);
		}

		[Test]
		public void Weighting_MarketCapWeight_NoManualSelection_Autocorrects()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddTicker(ticker3);

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25d, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(10));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.5d, 1561867200)
				.AddDividendsPerShare(0.5d, 1569816000)
				.AddDividendsPerShare(0.5d, 1577768400)
				.AddDividendsPerShare(0.5d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(20));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3)
				.AddDividendsPerShare(0.1d, 1561867200)
				.AddDividendsPerShare(0.1d, 1569816000)
				.AddDividendsPerShare(0.1d, 1577768400)
				.AddDividendsPerShare(0.1d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker3).AddClosePrice(4));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 3);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(33.33334, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(33.33333, result.Tickers[1].Weight);

			Assert.AreEqual(ticker3, result.Tickers[2].Ticker);
			Assert.AreEqual(33.33333, result.Tickers[2].Weight);
		}

		[Test]
		public void Weighting_MarketCapWeight_ManualSelection()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";

			var manualTicker = "ALEX";
			var manualWeight = 70d;

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddManualTicker(manualTicker, manualWeight);

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25d, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(10));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.5d, 1561867200)
				.AddDividendsPerShare(0.5d, 1569816000)
				.AddDividendsPerShare(0.5d, 1577768400)
				.AddDividendsPerShare(0.5d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(10));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 3);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(10m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(20m, result.Tickers[1].Weight);

			Assert.AreEqual(manualTicker, result.Tickers[2].Ticker);
			Assert.AreEqual(manualWeight, result.Tickers[2].Weight);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_ManualSelection_FilterOutManuallySelectedTickers()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";

			var manualTicker = "ALEX";
			var manualWeight = 70d;

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddTicker(manualTicker);
			AddManualTicker(manualTicker, manualWeight);

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25d, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(10));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.5d, 1561867200)
				.AddDividendsPerShare(0.5d, 1569816000)
				.AddDividendsPerShare(0.5d, 1577768400)
				.AddDividendsPerShare(0.5d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(10));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 3);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(10m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(20m, result.Tickers[1].Weight);

			Assert.AreEqual(manualTicker, result.Tickers[2].Ticker);
			Assert.AreEqual(manualWeight, result.Tickers[2].Weight);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_NoManualSelection_MissingData()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			var manualTicker = "ALEX";
			var manualWeight = 70d;

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddTicker(ticker3);
			AddTicker(manualTicker);
			AddManualTicker(manualTicker, manualWeight);

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1)
				.AddDividendsPerShare(0.25d, 1561867200)
				.AddDividendsPerShare(0.25d, 1569816000)
				.AddDividendsPerShare(0.25d, 1577768400)
				.AddDividendsPerShare(0.25d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(10));

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2)
				.AddDividendsPerShare(0.5d, 1561867200)
				.AddDividendsPerShare(0.5d, 1569816000)
				.AddDividendsPerShare(0.5d, 1577768400)
				.AddDividendsPerShare(0.5d, 1585627200));
			InsertData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(20));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 4);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(15m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(15m, result.Tickers[1].Weight);

			Assert.AreEqual(ticker3, result.Tickers[2].Ticker);
			Assert.AreEqual(0, result.Tickers[2].Weight);

			Assert.AreEqual(manualTicker, result.Tickers[3].Ticker);
			Assert.AreEqual(manualWeight, result.Tickers[3].Weight);
		}
	}
}
