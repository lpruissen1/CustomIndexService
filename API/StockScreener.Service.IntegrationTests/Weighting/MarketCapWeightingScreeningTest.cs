using NUnit.Framework;
using StockScreener.Core;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;
using System.Linq;

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
	}
}
