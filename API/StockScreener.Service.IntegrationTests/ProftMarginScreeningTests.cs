using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class ProfitMarginScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_ProfitMargin()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				ProfitMargin = new List<ProfitMargin> 
				{ 
					new ProfitMargin 
					{ 
						profitMargin = 0.4d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				ProfitMargin = new List<ProfitMargin>
				{
					new ProfitMargin
					{
						profitMargin = 0.05d
					}
				}
			});

			AddMarketToCustomIndex(stockIndex1);
			AddProfitMarginToCustomIndex(.5, .1);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}