using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class MarketCapScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_MarketCap()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				MarketCap = new List<MarketCap> 
				{ 
					new MarketCap 
					{ 
						marketCap = 1_000_000d 
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				MarketCap = new List<MarketCap>
				{
					new MarketCap
					{
						marketCap = 10_000d
					}
				}
			});

			AddMarketToCustomIndex(stockIndex1);
			AddMarketCapToCustomIndex(2_000_000d, 200_000d);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}