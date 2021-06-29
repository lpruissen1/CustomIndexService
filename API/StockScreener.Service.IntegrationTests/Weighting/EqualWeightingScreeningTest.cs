using NUnit.Framework;
using StockScreener.Core;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Service.IntegrationTests.Weighting
{
	[TestFixture]
	public class EqualWeightingScreeningTest : WeightingTestBase
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			weightingRequest.Option = WeightingOption.Equal;
		}

		[Test]
		public void Weighting_EqualWeight_NoManualSelection()
		{
			var ticker1 = "LEE";

			AddTicker(ticker1);

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 1);

			var entry = result.Tickers.First();

			Assert.AreEqual(entry.Ticker, ticker1);
			Assert.AreEqual(entry.Weight, 100m);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_NoManualSelection()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddTicker(ticker3);

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
		public void Weighting_EqualWeight_MultipleTickers_ManualSelection()
		{
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "SEE";

			var manualTicker = "ALEX";
			var manualWeight = 70d;

			AddTicker(ticker1);
			AddTicker(ticker2);
			AddTicker(ticker3);
			AddManualTicker(manualTicker, manualWeight);

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 4);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(10m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(10m, result.Tickers[1].Weight);

			Assert.AreEqual(ticker3, result.Tickers[2].Ticker);
			Assert.AreEqual(10m, result.Tickers[2].Weight);

			Assert.AreEqual(manualTicker, result.Tickers[3].Ticker);
			Assert.AreEqual(manualWeight, result.Tickers[3].Weight);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_ManualSelection_FilterOutManuallySelectedTickers()
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

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 4);

			Assert.AreEqual(ticker1, result.Tickers[0].Ticker);
			Assert.AreEqual(10m, result.Tickers[0].Weight);

			Assert.AreEqual(ticker2, result.Tickers[1].Ticker);
			Assert.AreEqual(10m, result.Tickers[1].Weight);

			Assert.AreEqual(ticker3, result.Tickers[2].Ticker);
			Assert.AreEqual(10m, result.Tickers[2].Weight);

			Assert.AreEqual(manualTicker, result.Tickers[3].Ticker);
			Assert.AreEqual(manualWeight, result.Tickers[3].Weight);
		}
	}
}
