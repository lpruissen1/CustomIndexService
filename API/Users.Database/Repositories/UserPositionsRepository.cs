using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserPositionsRepository : BaseRepository<UserPositions>, IUserPositionsRepository
	{
		public UserPositionsRepository(IMongoDBContext context) : base(context) { }

		public void CreatePosition(Guid userId, Position position)
		{
			FilterDefinition<UserPositions> filter = Builders<UserPositions>.Filter.Eq("UserId", userId);

			var update = Builders<UserPositions>.Update.Push("Positions", position);

			dbCollection.UpdateOne(filter, update);
		}

		public UserPositions GetByUserId(Guid userId)
		{
			FilterDefinition<UserPositions> filter = Builders<UserPositions>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public void UpdatePosition(Guid userId, Position position)
		{
			FilterDefinition<UserPositions> filter = Builders<UserPositions>.Filter.Eq("UserId", userId) & Builders<UserPositions>.Filter.ElemMatch(x => x.Positions, Builders<Position>.Filter.Eq(x => x.Ticker, position.Ticker));

			var update = Builders<UserPositions>.Update.Set("Positions.$.AveragePurchasePrice", position.AveragePurchasePrice).Set("Positions.$.Portfolios", position.Portfolios);

			dbCollection.UpdateOne(filter, update);
		}

		public void UpdatePosition(Guid userId, UserPositions positions)
		{
			FilterDefinition<UserPositions> filter = Builders<UserPositions>.Filter.Eq("UserId", userId);

			dbCollection.ReplaceOne(filter, positions);
		}
	}
}

