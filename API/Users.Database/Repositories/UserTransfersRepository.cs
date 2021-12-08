using Database.Core;
using Database.Repositories;
using MongoDB.Driver;
using System;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users.Database.Repositories
{
	public class UserTransfersRepository : BaseRepository<UserTransfers>, IUserTransfersRepository
	{
		public UserTransfersRepository(IMongoDBContext context) : base(context) { }

		public void AddTransfer(Guid userId, Transfer transfer)
		{
			FilterDefinition<UserTransfers> filter = Builders<UserTransfers>.Filter.Eq("UserId", userId);

			var update = Builders<UserTransfers>.Update.Push("Transfers", transfer);

			dbCollection.UpdateOneAsync(filter, update);
		}

		public void UpdateTransfer(Guid userId, Transfer transfer)
		{
			FilterDefinition<UserTransfers> filter = Builders<UserTransfers>.Filter.Eq("UserId", userId) & Builders<UserTransfers>.Filter.ElemMatch(x => x.Transfers, Builders<Transfer>.Filter.Eq(x => x.TransferId, transfer.TransferId));

			var update = Builders<UserTransfers>.Update.Set("Transfers.$.Status", transfer.Status).Set("Transfers.$.CancelledAt", transfer.CancelledAt).Set("Transfers.$.CompletedAt", transfer.CompletedAt).Set("Transfers.$.CreatedAt", transfer.CreatedAt);


			dbCollection.UpdateOneAsync(filter, update);
		}

		public UserTransfers GetByUserId(Guid userId)
		{
			FilterDefinition<UserTransfers> filter = Builders<UserTransfers>.Filter.Eq("UserId", userId);

			return dbCollection.Find(filter).FirstOrDefault();
		}
	}
}

