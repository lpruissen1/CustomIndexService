//using NUnit.Framework;
//using System;
//using System.Linq;
//using Users.Database.Model;

//namespace Users.Tests
//{
//	[TestFixture]
//	public class PositionAdditionHandlerTest : DbTestBase
//	{
//		PositionAdditionHandler sut;

//		[SetUp]
//		public void SetUp()
//		{
//			base.SetUp();
//			sut = new PositionAdditionHandler(UserPositionsRepository);
//		}

//		[Test]
//		public void CreateNewPosition_InsertsNewPosition()
//		{
//			var portfolioGuid = Guid.NewGuid();
//			var ticker = "LEE";
//			var averagePrice = 103.39m;
//			var quantity = 10;
//			var newPosition = new Position(ticker, averagePrice, portfolioGuid, quantity);
//			UserPositionsRepository.Create(new UserPositions(userId));

//			sut.AddPosition(userId, newPosition);

//			var result = UserPositionsRepository.GetByUserId(userId).Positions.First(x => x.Ticker == ticker);

//			Assert.AreEqual(result.AveragePurchasePrice, averagePrice);
//			Assert.AreEqual(result.Portfolios.First().Value, quantity);
//			Assert.AreEqual(result.Portfolios.First().Key, portfolioGuid.ToString());
//		}

//		[Test]
//		public void AddToExistingPosition_NewPortfolio_AdjustsAverageCost_AddsPortfolioToList()
//		{
//			var portfolioGuid = Guid.NewGuid();
//			var ticker = "LEE";
//			var averagePrice = 75m;
//			var quantity = 10;
//			var existingPosition = new Position(ticker, averagePrice, portfolioGuid, quantity);

//			UserPositionsRepository.Create(new UserPositions(userId) { Positions = new System.Collections.Generic.List<Position> { new Position(ticker, averagePrice, portfolioGuid, quantity) } });

//			var newPortfolioGuid = Guid.NewGuid();
//			var newAveragePrice = 100;
//			var newQuantity = 5;
//			var newPosition = new Position(ticker, newAveragePrice, newPortfolioGuid, newQuantity);

//			sut.AddPosition(userId, newPosition);

//			var result = UserPositionsRepository.GetByUserId(userId).Positions.First(x => x.Ticker == ticker);

//			var expectedAveragePurchasePrice = GetAveragePurchasePrice(averagePrice, quantity, newAveragePrice, newQuantity);

//			Assert.AreEqual(expectedAveragePurchasePrice, result.AveragePurchasePrice);

//			Assert.AreEqual(result.Portfolios.Count, 2);

//			AssertBlah(result, portfolioGuid.ToString(), quantity);

//			AssertBlah(result, newPortfolioGuid.ToString(), newQuantity);
//		}

//		[Test]
//		public void AddToExistingPosition_SamePortfolio_AdjustsAverageCost_AddsPortfolioToList()
//		{
//			var portfolioGuid = Guid.NewGuid();
//			var ticker = "LEE";
//			var averagePrice = 75m;
//			var quantity = 10;
//			var existingPosition = new Position(ticker, averagePrice, portfolioGuid, quantity);

//			UserPositionsRepository.Create(new UserPositions(userId) { Positions = new System.Collections.Generic.List<Position> { new Position(ticker, averagePrice, portfolioGuid, quantity) } });

//			var newAveragePrice = 60;
//			var newQuantity = 5;
//			var newPosition = new Position(ticker, newAveragePrice, portfolioGuid, newQuantity);

//			sut.AddPosition(userId, newPosition);

//			var result = UserPositionsRepository.GetByUserId(userId).Positions.First(x => x.Ticker == ticker);

//			var expectedAveragePurchasePrice = GetAveragePurchasePrice(averagePrice, quantity, newAveragePrice, newQuantity);

//			Assert.AreEqual(expectedAveragePurchasePrice, result.AveragePurchasePrice);

//			Assert.AreEqual(result.Portfolios.Count, 1);

//			AssertBlah(result, portfolioGuid.ToString(), quantity + newQuantity);
//		}

//		private void AssertBlah(Position position, string portfolio, decimal quantity)
//		{
//			Assert.IsTrue(position.Portfolios.ContainsKey(portfolio));
//			Assert.AreEqual(quantity, position.Portfolios[portfolio]);
//		}

//		private decimal GetAveragePurchasePrice(decimal firstPrice, decimal firstQuantity, decimal secondPrice, decimal secondQuantity)
//		{
//			return ((firstPrice * firstQuantity) + (secondPrice * secondQuantity)) / (firstQuantity + secondQuantity);
//		}
//	}
//}

