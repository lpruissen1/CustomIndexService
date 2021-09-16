using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserAccountsRepository : BaseRepository<UserAccounts>, IUserAccountsRepository
	{
		public UserAccountsRepository(IMongoDBContext context) : base(context) { }

		public UserAccounts GetByUserId(Guid userId)
		{
			FilterDefinition<UserAccounts> filter = Builders<UserAccounts>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}
}

