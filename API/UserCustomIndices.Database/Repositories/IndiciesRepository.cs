using Database.Core;
using Database.Model.User.CustomIndices;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class IndiciesRepository : BaseRepository<CustomIndex>, IIndicesRepository
    {
        public IndiciesRepository(IMongoDBContext context) : base(context) { }

        public async Task<CustomIndex> Get(string userId, string indexId)
        {
            return await dbCollection.FindAsync(i => i.IndexId == indexId && i.UserId == userId).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomIndex>> GetAllForUser(string userId)
        {
            return await dbCollection.FindAsync(i => i.UserId == userId.ToString()).Result.ToListAsync();
        }

		public void UpdateIndex(string userId, CustomIndex updatedIndex)
		{
			dbCollection.FindOneAndReplace(i => i.UserId == userId.ToString() && i.IndexId == updatedIndex.IndexId, updatedIndex);
		}

		public void DeleteIndex(string userId, string indexId)
		{
			var builder = Builders<CustomIndex>.Filter;
			var userFilter = builder.Eq(i => i.UserId, userId);
			var indexFilter = builder.Eq(i => i.IndexId, indexId);
			var combinedFilter = builder.And(userFilter, indexFilter);
			var update = Builders<CustomIndex>.Update.Set(i => i.Active, false);

			dbCollection.FindOneAndUpdate(combinedFilter, update);
		}
    }
}

