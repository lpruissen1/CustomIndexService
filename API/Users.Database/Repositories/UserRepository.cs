﻿using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
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
}

