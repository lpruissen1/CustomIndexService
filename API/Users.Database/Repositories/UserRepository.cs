using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		public UserRepository(IMongoDBContext context) : base(context) { }

		public User Get(Guid userId)
		{
			FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public User GetByUsername(string userName)
		{
			FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserName", userName);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public User GetByUserId(string userId)
		{
			FilterDefinition<User> filter = Builders<User>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}

		public bool UpgradeUser(User user)
		{
			var result = dbCollection.FindOneAndReplace(i => i.UserId == user.UserId, user);

			return result is not null ? true : false;
		}
	}

	public class UserDocumentsRepository : BaseRepository<UserDocuments>, IUserDocumentsRepository
	{
		public UserDocumentsRepository(IMongoDBContext context) : base(context) { }

		public UserDocuments GetByUserId(string userId)
		{
			FilterDefinition<UserDocuments> filter = Builders<UserDocuments>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}

	public class UserAccountsRepository : BaseRepository<UserAccounts>, IUserAccountsRepository
	{
		public UserAccountsRepository(IMongoDBContext context) : base(context) { }

		public UserAccounts GetByUserId(string userId)
		{
			FilterDefinition<UserAccounts> filter = Builders<UserAccounts>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}

	public class UserDisclosuresRepository : BaseRepository<UserDisclosures>, IUserDisclosuresRepository
	{
		public UserDisclosuresRepository(IMongoDBContext context) : base(context) { }

		public UserAccounts Create(UserAccounts obj)
		{
			throw new NotImplementedException();
		}

		public UserDisclosures GetByUserId(string userId)
		{
			FilterDefinition<UserDisclosures> filter = Builders<UserDisclosures>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}
}

