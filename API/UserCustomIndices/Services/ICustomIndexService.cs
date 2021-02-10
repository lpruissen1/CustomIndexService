using Database.Model.User.CustomIndices;
using System;
using System.Collections.Generic;

namespace UserCustomIndices.Services
{
    public interface ICustomIndexService
    {
        public CustomIndex Get(string indexId);
        public List<CustomIndex> Get(Guid userid);
        void Create(CustomIndex customIndex);
        CustomIndex Update(Guid id, CustomIndex customIndexUpdated);
        void Remove(CustomIndex index);
        void Remove(string id);
    }
}