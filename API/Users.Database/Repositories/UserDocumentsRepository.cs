using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserDocumentsRepository : BaseRepository<UserDocuments>, IUserDocumentsRepository
	{
		public UserDocumentsRepository(IMongoDBContext context) : base(context) { }

		public UserDocuments GetByUserId(Guid userId)
		{
			FilterDefinition<UserDocuments> filter = Builders<UserDocuments>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}
}

