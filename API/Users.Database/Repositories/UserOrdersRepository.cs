using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserOrdersRepository : BaseRepository<UserOrders>, IUserOrdersRepository
	{
		public UserOrdersRepository(IMongoDBContext context) : base(context) { }

		public UserOrders GetByUserId(Guid userId)
		{
			FilterDefinition<UserOrders> filter = Builders<UserOrders>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public void AddOrders(Guid userId, List<Order> orders)
		{
			FilterDefinition<UserOrders> filter = Builders<UserOrders>.Filter.Eq("UserId", userId);

			var update = Builders<UserOrders>.Update.PushEach("Orders", orders);

			dbCollection.UpdateOneAsync(filter, update);
		}
	}
}

