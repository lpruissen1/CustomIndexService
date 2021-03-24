using Database.Model.User.CustomIndices;
using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Model.CompanyInfo;
using StockScreener.Database.Model.StockIndex;
using StockScreener.Database.Repos;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
    public class StockScreenerTests : StockScreenerServiceTestBase
	{
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ScreenByStockIndex_SingleIndexTest()
        {
			var stockIndex1 = "Lee's Index";
			var stockIndex2 = "Lee's second Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1, ticker2 } });
            AddStockIndex(new StockIndex { Name = stockIndex2, Tickers = new[] { ticker3 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2 });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex1
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result.First().Ticker);
			Assert.AreEqual(ticker2, result.Last().Ticker);
        }

        [Test]
        public void ScreenByStockIndex_MultipleMIndicesTest()
        {
			var stockIndex1 = "Lee's Index";
			var stockIndex2 = "Lee's second Index";
			var stockIndex3 = "Lee's third Index";

			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";

			AddStockIndex(new StockIndex { Name = stockIndex1, Tickers = new[] { ticker1 } });
            AddStockIndex(new StockIndex { Name = stockIndex2, Tickers = new[] { ticker2 } });
            AddStockIndex(new StockIndex { Name = stockIndex3, Tickers = new[] { ticker3 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker3 });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex1,
						stockIndex3
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(2, result.Count);

			Assert.AreEqual(ticker1, result.First().Ticker);
			Assert.AreEqual(ticker3, result.Last().Ticker);
        }

        [Test]
        public void ScreenStockBySectorTest()
        {
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var stockIndex = "Lee's Index";
			var sector1 = "Energy";
			var industry1 = "Oil";
			var sector2 = "Materials";
			var industry2 = "Plastics";

			AddStockIndex(new StockIndex { Name = stockIndex, Tickers = new[] { ticker1, ticker2 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1, Sector = sector1, Industry = industry1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2, Sector = sector2, Industry = industry2 });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex
					}
				},
				SectorAndIndsutry = new Sectors
				{
					SectorGroups = new[]
					{
						new Sector { Name = sector1}
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			var security = result.First();

			Assert.AreEqual(ticker1, security.Ticker);
			Assert.AreEqual(sector1, security.Sector);
        }

        [Test]
        public void ScreenStockByIndustryTest()
        {
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";
			var stockIndex = "Lee's Index";
			var energySector = "Energy";
			var energyIndustry1 = "Oil";
			var energyIndustry2 = "Solar";
			var materialsSector = "Materials";
			var materialsIndustry1 = "Plastics";

			AddStockIndex(new StockIndex { Name = stockIndex, Tickers = new[] { ticker1, ticker2, ticker3 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1, Sector = energySector, Industry = energyIndustry1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2, Sector = energySector, Industry = energyIndustry2 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker3, Sector = materialsSector, Industry = materialsIndustry1 });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex
					}
				},
				SectorAndIndsutry = new Sectors
				{
					SectorGroups = new[]
					{
						new Sector { Name = energySector, Industries = new[] { energyIndustry1 }}
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(1, result.Count);

			var security = result.First();

			Assert.AreEqual(ticker1, security.Ticker);
			Assert.AreEqual(energySector, security.Sector);
			Assert.AreEqual(energyIndustry1, security.Industry);
        }

        [Test]
        public void ScreenStockBy_SectorAndIndustryTest()
        {
			var stockIndex = "Lee's Index";
			var ticker1 = "LEE";
			var ticker2 = "PEE";
			var ticker3 = "EEL";

			var energySector = "Energy";
			var energyIndustry1 = "Oil";
			var energyIndustry2 = "Solar";

			var materialsSector = "Materials";
			var materialsIndustry = "Plastics";

			AddStockIndex(new StockIndex { Name = stockIndex, Tickers = new[] { ticker1, ticker2, ticker3 } });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker1, Sector = energySector, Industry = energyIndustry1 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker2, Sector = energySector, Industry = energyIndustry2 });
			AddCompanyInfo(new CompanyInfo { Ticker = ticker3, Sector = materialsSector, Industry = materialsIndustry });

			var customIndex = new CustomIndex()
			{
				Markets = new ComposedMarkets
				{
					Markets = new[]
					{
						stockIndex
					}
				},
				SectorAndIndsutry = new Sectors
				{
					SectorGroups = new[]
					{
						new Sector { Name = energySector, Industries = new[] { energyIndustry1 }},
						new Sector { Name = materialsSector}
					}
				}
			};

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context)));

			var result = sut.Screen(customIndex);

			Assert.AreEqual(2, result.Count);

			var security = result.First();

			Assert.AreEqual(ticker1, security.Ticker);
			Assert.AreEqual(energySector, security.Sector);
			Assert.AreEqual(energyIndustry1, security.Industry);

			security = result.Last();

			Assert.AreEqual(ticker3, security.Ticker);
			Assert.AreEqual(materialsSector, security.Sector);
			Assert.AreEqual(materialsIndustry, security.Industry);
        }
    }
}