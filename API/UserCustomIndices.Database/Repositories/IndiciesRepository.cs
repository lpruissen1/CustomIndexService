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

		public bool UpdateIndex(string userId, CustomIndex updatedIndex)
		{
			var result = dbCollection.FindOneAndReplace(i => i.UserId == userId.ToString() && i.IndexId == updatedIndex.IndexId, updatedIndex);

			return result is not null ? true : false;
		}

		public bool DeleteIndex(string userId, string indexId)
		{
			var result = dbCollection.FindOneAndDelete(i => i.UserId == userId.ToString() && i.IndexId == indexId);
			
			return result is not null ? true : false;
		}
    }
}

