using NUnit.Framework;
using StockScreener.Core;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests.Weighting
{
    [TestFixture]
    public class WeightByMarketCapTest : WeightingFatPeopleTestBase
	{
		[Test]
		public void WeightByMarketCap()
		{
			var ticker1 = "LEE";
			var marketCap1 = 333_333d;
			var ticker2 = "PEE";
			var marketCap2 = 222_222d;
			var ticker3 = "Alex";
			var marketCap3 = 333_333d;
			var ticker4 = "Smalex";
			var marketCap4 = 111_112d;

			InsertData(StockFinancialsCreator.GetStockFinancials(ticker1).AddMarketCap(marketCap1));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker2).AddMarketCap(marketCap2));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker3).AddMarketCap(marketCap3));
			InsertData(StockFinancialsCreator.GetStockFinancials(ticker4).AddMarketCap(marketCap4));

			var result = sut.Weight(new List<string>() { ticker1, ticker2, ticker3, ticker4 }, new List<BaseDatapoint> { BaseDatapoint.MarketCap });

			Assert.AreEqual(.33333, result[ticker1]);
			Assert.AreEqual(.22222, result[ticker2]);
			Assert.AreEqual(.33333, result[ticker3]);
			Assert.AreEqual(.11111, result[ticker4]);
		}
	}
}
