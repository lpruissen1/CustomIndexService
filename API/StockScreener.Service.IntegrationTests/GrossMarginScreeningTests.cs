using Database.Model.User.CustomIndices;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class GrossMarginsScreeningTest : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_GrossMargin()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				GrossMargin = new List<GrossMargin> 
				{ 
					new GrossMargin 
					{ 
						grossMargin = 0.4d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				GrossMargin = new List<GrossMargin>
				{
					new GrossMargin
					{
						grossMargin = 0.05d
					}
				}
			}) ;


			AddMarketToCustomIndex(stockIndex1);
            AddGrossMarginToCustomIndex(0.5, 0.1);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}