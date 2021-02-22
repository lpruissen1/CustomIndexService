using Database.Model.User.CustomIndices;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class IndiciesRepository : BaseRepository<CustomIndex>, IIndicesRepository
    {
        public IndiciesRepository(IMongoCustomIndexDBContext context) : base(context) { }

        public async Task<CustomIndex> Get(Guid userId, string id)
        {
            return await _dbCollection.FindAsync(i => i.Id == id && i.UserId == userId.ToString()).Result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CustomIndex>> GetAllForUser(Guid userId)
        {
            return await _dbCollection.FindAsync(i => i.UserId == userId.ToString()).Result.ToListAsync();
        }
    }
}

