using NUnit.Framework;
using StockScreener.Database.Model.StockFinancials;
using StockScreener.Database.Model.StockIndex;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
	public class WorkingCapitalScreeningTests : StockScreenerServiceTestBase
	{
		[Test]
		public void ScreenByStockIndex_WorkingCapital()
		{
			var stockIndex1 = "Lee's Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
			AddStockFinancials(new StockFinancials 
			{ 
				Ticker = ticker1,
				WorkingCapital = new List<WorkingCapital> 
				{ 
					new WorkingCapital 
					{ 
						workingCapital = 10_000_000d
					} 
				} 
			});

			AddStockFinancials(new StockFinancials
			{
				Ticker = ticker2,
				WorkingCapital = new List<WorkingCapital>
				{
					new WorkingCapital
					{
						workingCapital = 1_000_000d
					}
				}
			});

			AddMarketToCustomIndex(stockIndex1);
			AddWorkingCapitalToCustomIndex(15_000_000, 5_000_000);

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			Assert.AreEqual(ticker1, result[0].Ticker);
		}
	}
}