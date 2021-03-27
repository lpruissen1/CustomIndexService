using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Core
{
    public interface IBaseRepository<TEntity> where TEntity : DbEntity
    {
        Task Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        Task<TEntity> Get(string id);
    }
}
