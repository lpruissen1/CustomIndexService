using Database.Model.User.CustomIndices;
using System;
using System.Collections.Generic;

namespace UserCustomIndices.Services
{
    public interface ICustomIndexService
    {
        public CustomIndex Get(Guid userId, string indexId);
        public List<CustomIndex> Get(Guid userid);
        void Create(Guid userId, CustomIndex customIndex);
        CustomIndex Update(Guid id, CustomIndex customIndexUpdated);
        bool Remove(Guid userId, string id);
    }
}