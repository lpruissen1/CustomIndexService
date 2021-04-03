using Database.Model.User.CustomIndices;
using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class PayoutRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_PayoutRatio()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				PayoutRatio = new List<PayoutRatio> 
				{ 
					new PayoutRatio 
					{ 
						payoutRatio = 0.1d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				PayoutRatio = new List<PayoutRatio>
				{
					new PayoutRatio
					{
						payoutRatio = 0.2d
					}
				}
			}) ;

			AddMarketToCustomIndex(stockIndex1);
			AddPayoutRatioToCustomIndex(0.15, 0);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}