using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserDisclosuresRepository : BaseRepository<UserDisclosures>, IUserDisclosuresRepository
	{
		public UserDisclosuresRepository(IMongoDBContext context) : base(context) { }

		public UserAccounts Create(UserAccounts obj)
		{
			throw new NotImplementedException();
		}

		public UserDisclosures GetByUserId(Guid userId)
		{
			FilterDefinition<UserDisclosures> filter = Builders<UserDisclosures>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}
}

