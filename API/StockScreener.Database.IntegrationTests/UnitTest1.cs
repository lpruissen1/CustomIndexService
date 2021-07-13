using Core;
using NUnit.Framework;
using StockScreener.Database.Model.Price;
using StockScreener.Database.Repos;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;
using System.Linq;

namespace StockScreener.Database.IntegrationTests
{
	[TestFixture]
    public class PriceDataRepositoryTest : RepoTestBase
    {
		PriceDataRepository sut;

		[SetUp]
		protected override void SetUp()
		{
			base.SetUp();
			sut = new PriceDataRepository(mongoContextFactory);
		}

        [Test]
        public void GetClosePriceOverTimePeriod_PullsRelevantPriceData()
        {
			var ticker1 = "LEE";
			var ticker2 = "PEE";

			InserPriceData(PriceDataCreator.GetDailyPriceData(ticker1).AddClosePrice(143.69, 1609480830).AddClosePrice(149.20, 1609480831));
			InserPriceData(PriceDataCreator.GetDailyPriceData(ticker2).AddClosePrice(27.92, 1609480830).AddClosePrice(21.45, 1617411630));

			var result = sut.GetClosePriceOverTimePeriod<DayPriceData>(new List<string> { ticker1, ticker2}, TimePeriod.HalfYear).ToList();

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
			Assert.AreEqual(1, result[0].Candle.Count);
			Assert.AreEqual(ticker2, result[1].Ticker);
			Assert.AreEqual(1, result[1].Candle.Count);
		}
	}


}
