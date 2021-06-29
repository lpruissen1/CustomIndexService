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

			Assert.AreEqual(entry.Key, ticker1);
			Assert.AreEqual(entry.Value, 100m);
		}

		[Test]
		public void Weighting_EqualWeight_MultipleTickers_NoManualSelection()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE",
				"SEE"
			};

			tickers.ForEach(x => AddTicker(x));

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
		public void Weighting_EqualWeight_MultipleTickers_ManualSelection()
		{
			var tickers = new List<string> {
				"LEE",
				"PEE",
				"SEE"
			};

			var manualTicker = "ALEX";
			var manualWeight = 70m;

			tickers.ForEach(x => AddTicker(x));

			AddManualTicker(manualTicker, manualWeight);

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
