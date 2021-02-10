using Database.Model.User.CustomIndices;
using System;
using System.Collections.Generic;
using UserCustomIndices.Services;

namespace UserCustomIndicesTests.Controllers.Fakes
{
    class CustomIndexServiceFake : ICustomIndexService
    {
        private List<CustomIndex> customIndexCollection;
        private Dictionary<Guid, List<string>> userIndicesCollection;

        public CustomIndexServiceFake(List<CustomIndex> customIndices, Dictionary<Guid, List<string>> userIndices) {
            this.userIndicesCollection = userIndices;
            this.customIndexCollection = customIndices;
        }

        public void Create(CustomIndex customIndex)
        {
            customIndexCollection.Add(customIndex);
        }

        public CustomIndex Get(string indexId)
        {
            return customIndexCollection.Find(x => x.Id == indexId);
        }

        public List<CustomIndex> Get(Guid userid)
        {
            List<CustomIndex> indices = new List<CustomIndex>();
            var ids = userIndicesCollection.GetValueOrDefault(userid);
            if (ids is null)
                return null;

            foreach(var id in ids)
            {
               indices.Add(customIndexCollection.Find(x => x.Id == id));
            }

            return indices;
        }

        public void Remove(CustomIndex bookIn)
        {
            customIndexCollection.Remove(bookIn);
        }

        public void Remove(string id)
        {
            customIndexCollection.RemoveAll(x => x.Id == id);
        }

        public CustomIndex Update(Guid clientId, CustomIndex customIndexUpdated)
        {
            var ids = userIndicesCollection.GetValueOrDefault(clientId);
            if (ids is null)
                return null;

            var index = customIndexCollection.Find(x => x.Id == customIndexUpdated.Id);
            customIndexCollection.Remove(index);

            customIndexCollection.Add(customIndexUpdated);
            return customIndexUpdated;
        }

        public bool Remove(Guid userId, string id)
        {
            var ids = userIndicesCollection.GetValueOrDefault(userId);
            if (ids is null|| !ids.Contains(id))
                return false;

            customIndexCollection.RemoveAll(x => x.Id == id);

            return true;
        }
    }
}
