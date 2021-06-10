using System.Collections.Generic;

namespace Database.Core
{
    public interface IBaseRepository<TEntity> where TEntity : DbEntity
    {
		TEntity Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        TEntity Get(string id);
        IEnumerable<TEntity> Get();
        void Clear(string collectionName);
	}
}
