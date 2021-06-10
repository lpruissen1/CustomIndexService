using Database.Core;
using MongoDB.Driver;
using System.Collections.Generic;

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

        public TEntity Create(TEntity obj)
        {
            dbCollection.InsertOne(obj);

			return obj;
        }

        public void Delete(string id)
        {
			dbCollection.FindOneAndDelete(x => x.Id == id);
        }

        public virtual TEntity Get(string ticker)
        {
            FilterDefinition<TEntity> filter = Builders<TEntity>.Filter.Eq("Ticker", ticker);

            return dbCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> Get()
        {
            return dbCollection.Find(Builders<TEntity>.Filter.Empty).ToEnumerable();
        }

        public void Update(TEntity obj)
        {
			dbCollection.FindOneAndReplace(x => x.Id == obj.Id, obj);
        }

        public void Clear(string collectionName)
        {
            mongoContext.DropCollection(collectionName);
        }
    }
}
