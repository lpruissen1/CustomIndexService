using Database.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : DbEntity
    {

        protected readonly IMongoDBContext _mongoContext;
        protected IMongoCollection<TEntity> _dbCollection;

        protected BaseRepository(IMongoDBContext context)
        {
            _mongoContext = context;
            _dbCollection = _mongoContext.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public Task Create(TEntity obj)
        {
            return _dbCollection.InsertOneAsync(obj);
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Get(string id)
        {
            return await _dbCollection.FindAsync(i => i.Id == id).Result.FirstOrDefaultAsync();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
