using Database.Core;
using Database.Model.User.CustomIndices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IIndicesRepository : IBaseRepository<CustomIndex>
    {
        Task<CustomIndex> Get(string userId, string id);
        Task<IEnumerable<CustomIndex>> GetAllForUser(string userId);
    }
}
