using Database.Model.User.CustomIndices;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class FreeCashFlowScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_FreeCashFlow()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				FreeCashFlow = new List<FreeCashFlow> 
				{ 
					new FreeCashFlow 
					{ 
						freeCashFlow = 1_000_000_000d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				FreeCashFlow = new List<FreeCashFlow>
				{
					new FreeCashFlow
					{
						freeCashFlow = 1_000_000d
					}
				}
			}) ;

			AddMarketToCustomIndex(stockIndex1);
			AddFreeCashFlowToCustomIndex(10_000_000_000, 10_000_000);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}