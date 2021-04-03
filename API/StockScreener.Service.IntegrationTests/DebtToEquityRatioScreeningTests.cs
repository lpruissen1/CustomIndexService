using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class DebtToEquityRatioScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_DebtToEquityRatio()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				DebtToEquityRatio = new List<DebtToEquityRatio> 
				{ 
					new DebtToEquityRatio 
					{ 
						debtToEquityRatio = 0.75d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				DebtToEquityRatio = new List<DebtToEquityRatio>
				{
					new DebtToEquityRatio
					{
						debtToEquityRatio = 2.5d
					}
				}
			}) ;

			AddMarketToCustomIndex(stockIndex1);
			AddDebtToEquityRatioToCustomIndex(1, 0);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}