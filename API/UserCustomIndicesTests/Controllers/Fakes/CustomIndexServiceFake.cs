using Database.Model.User.CustomIndices;
using System;
using System.Collections.Generic;
using UserCustomIndices.Services;

namespace UserCustomIndicesTests.Controllers.Fakes
{
    class CustomIndexServiceFake : ICustomIndexService
    {
        private List<CustomIndex> CustomIndexCollection;
        private Dictionary<Guid, List<string>> UserIndicesCollection;

        public CustomIndexServiceFake(List<CustomIndex> customIndices, Dictionary<Guid, List<string>> userIndicesCollection) {
            UserIndicesCollection = userIndicesCollection;
            CustomIndexCollection = customIndices;
        }

        public void Create(CustomIndex customIndex)
        {
            CustomIndexCollection.Add(customIndex);
        }

        public CustomIndex Get(string indexId)
        {
            return CustomIndexCollection.Find(x => x.Id == indexId);
        }

        public List<CustomIndex> Get(Guid userid)
        {
            List<CustomIndex> indices = new List<CustomIndex>();
            var ids = UserIndicesCollection.GetValueOrDefault(userid);
            if (ids is null)
                return null;

            foreach(var id in ids)
            {
               indices.Add(CustomIndexCollection.Find(x => x.Id == id));
            }

            return indices;
        }

        public void Remove(CustomIndex bookIn)
        {
            CustomIndexCollection.Remove(bookIn);
        }

        public void Remove(string id)
        {
            CustomIndexCollection.RemoveAll(x => x.Id == id);
        }

        public void Update(string id, CustomIndex customIndexUpdated)
        {
            throw new NotImplementedException();
        }
    }
}
