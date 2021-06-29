using NUnit.Framework;
using StockScreener.Core;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests.Weighting
{
	[TestFixture]
	public class MarketCapWeightingScreeningTest : WeightingTestBase
	{
		[SetUp]
		public override void SetUp()
		{
			base.SetUp();

			weightingRequest.Option = WeightingOption.MarketCap;
		}

		[Test]
		public void Weighting_MarketCapWeight_NoManualSelection()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE"
			};

			AddTicker(tickers[0]);
			AddTicker(tickers[1]);

			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[0]).AddMarketCap(1_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[1]).AddMarketCap(9_000_000d));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 2);

			decimal value;

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[0], out value));
			Assert.AreEqual(10m, value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[1], out value));
			Assert.AreEqual(90m, value);
		}

		[Test]
		public void Weighting_MarketCapWeight_NoManualSelection_Autocorrects()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE",
				"SEE"
			};

			AddTicker(tickers[0]);
			AddTicker(tickers[1]);
			AddTicker(tickers[2]);

			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[0]).AddMarketCap(3_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[1]).AddMarketCap(3_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[2]).AddMarketCap(3_000_000d));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 3);

			decimal value;

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[0], out value));
			Assert.AreEqual(33.33334, value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[1], out value));
			Assert.AreEqual(33.33333, value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[2], out value));
			Assert.AreEqual(33.33333, value);
		}

		[Test]
		public void Weighting_MarketCapWeight_ManualSelection()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE"
			};

			var manualTicker = "ALEX";
			var manualWeight = 70m;

			AddTicker(tickers[0]);
			AddTicker(tickers[1]);
			AddManualTicker(manualTicker, manualWeight);

			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[0]).AddMarketCap(1_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[1]).AddMarketCap(9_000_000d));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 3);

			decimal value;

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[0], out value));
			Assert.AreEqual(30m * (1m/10m), value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[1], out value));
			Assert.AreEqual(30m * (9m/10m), value);

			Assert.IsTrue(result.Tickers.TryGetValue(manualTicker, out value));
			Assert.AreEqual(manualWeight, value);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_ManualSelection_FilterOutManuallySelectedTickers()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE",
				"SEE",
				"ALEX"
			};

			var manualTicker = "ALEX";
			var manualWeight = 70m;

			tickers.ForEach(x => AddTicker(x));

			AddManualTicker(manualTicker, manualWeight);

			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[0]).AddMarketCap(3_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[1]).AddMarketCap(3_000_000d));
			InsertData(StockFinancialsCreator.GetStockFinancials(tickers[2]).AddMarketCap(3_000_000d));

			var result = sut.Weighting(weightingRequest);

			Assert.AreEqual(result.Tickers.Count, 4);

			decimal value;

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[0], out value));
			Assert.AreEqual(10m, value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[1], out value));
			Assert.AreEqual(10m, value);

			Assert.IsTrue(result.Tickers.TryGetValue(tickers[2], out value));
			Assert.AreEqual(10m, value);

			Assert.IsTrue(result.Tickers.TryGetValue(manualTicker, out value));
			Assert.AreEqual(value, manualWeight);
		}
	}
}
