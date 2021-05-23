using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class PasswordListRepository : BaseRepository<PasswordList>, IPasswordListRepository
	{
		public PasswordListRepository(IMongoDBContext context) : base(context) { }

		public PasswordList Get(Guid userId)
		{
			FilterDefinition<PasswordList> filter = Builders<PasswordList>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public PasswordList Update(PasswordList passwordList)
		{
			FilterDefinition<PasswordList> filter = Builders<PasswordList>.Filter.Eq("UserId", passwordList.UserId);

			return dbCollection.FindOneAndReplace(filter, passwordList);
		}
	}
}

