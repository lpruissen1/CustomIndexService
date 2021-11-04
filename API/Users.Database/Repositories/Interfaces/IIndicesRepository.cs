using Database.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Database.Model.CustomIndex;

namespace Users.Database.Repositories.Interfaces
{
	public interface IIndicesRepository : IBaseRepository<CustomIndex>
    {
        Task<CustomIndex> Get(string userId, string id);
        Task<IEnumerable<CustomIndex>> GetAllForUser(string userId);
		bool UpdateIndex(string userId, CustomIndex updatedIndex);
		bool DeleteIndex(string userId, string indexId);
    }
}
