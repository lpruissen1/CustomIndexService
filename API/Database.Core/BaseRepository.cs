using Database.Core;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : DbEntity
    {

        protected readonly IMongoDBContext mongoContext;
        protected IMongoCollection<TEntity> dbCollection;

        protected BaseRepository(IMongoDBContext context)
        {
            mongoContext = context;
            dbCollection = mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task Create(TEntity obj)
        {
            return dbCollection.InsertOneAsync(obj);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(string id)
        {
            return await dbCollection.FindAsync(i => i.Id == id).Result.FirstOrDefaultAsync();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
