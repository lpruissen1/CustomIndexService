﻿using Database.Repositories;
using NUnit.Framework;
using StockScreener.Database.Repos;
using StockScreener.SecurityGrabber;
using StockScreener.Service.IntegrationTests.StockDataHelpers;
using System.Linq;

namespace StockScreener.Service.IntegrationTests
{
    [TestFixture]
    public class SectorAndIndustryScreeningTests : StockScreenerServiceTestBase
	{
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

			InsertData(StockIndexCreator.GetStockIndex(stockIndex).AddTicker(ticker1).AddTicker(ticker2));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker1).AddSector(sector1).AddIndustry(industry1));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker2).AddSector(sector2).AddIndustry(industry2));

			AddMarketToCustomIndex(stockIndex);
			AddSectorToCustomIndex(sector1);

			sut = new StockScreenerService(new SecuritiesGrabber(new StockFinancialsRepository(context), new CompanyInfoRepository(context), new StockIndexRepository(context), new PriceDataRepository(context)));

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

			InsertData(StockIndexCreator.GetStockIndex(stockIndex).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker1).AddSector(energySector).AddIndustry(energyIndustry1));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker2).AddSector(energySector).AddIndustry(energyIndustry2));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker3).AddSector(materialsSector).AddIndustry(materialsIndustry1));

			AddMarketToCustomIndex(stockIndex);
			AddIndustryToCustomIndex(energyIndustry1);

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

			InsertData(StockIndexCreator.GetStockIndex(stockIndex).AddTicker(ticker1).AddTicker(ticker2).AddTicker(ticker3));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker1).AddSector(energySector).AddIndustry(energyIndustry1));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker2).AddSector(energySector).AddIndustry(energyIndustry2));
			InsertData(CompanyInfoCreator.GetCompanyInfo(ticker3).AddSector(materialsSector).AddIndustry(materialsIndustry));

			AddMarketToCustomIndex(stockIndex);
			AddIndustryToCustomIndex(energyIndustry1);
			AddSectorToCustomIndex(materialsSector);

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