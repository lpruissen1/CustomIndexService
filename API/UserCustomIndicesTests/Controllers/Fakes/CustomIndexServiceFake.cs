using Database.Model.User.CustomIndices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCustomIndices.Model.Response;
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

        Task<ActionResult<List<CustomIndex>>> GetAllForUser(Guid userid)
        {
            throw new NotImplementedException();
        }

        //public List<CustomIndex> GetAllForUser(Guid userid)
        //{
        //    List<CustomIndex> indices = new List<CustomIndex>();
        //    var ids = userIndicesCollection.GetValueOrDefault(userid);
        //    if (ids is null)
        //        return null;

        //    foreach (var id in ids)
        //    {
        //        indices.Add(customIndexCollection.Find(x => x.Id == id));
        //    }

        //    return indices;
        //}

        public void Create(Guid userId, CustomIndex customIndex)
        {
            customIndexCollection.Add(customIndex);
        }

        public CustomIndex GetIndex(Guid userId, string indexId)
        {
            return customIndexCollection.Find(x => x.Id == indexId);
        }

        public void Remove(CustomIndex bookIn)
        {
            customIndexCollection.Remove(bookIn);
        }

        public void Remove(string id)
        {
            customIndexCollection.RemoveAll(x => x.Id == id);
        }

        public CustomIndex UpdateIndex(Guid clientId, CustomIndex customIndexUpdated)
        {
            var ids = userIndicesCollection.GetValueOrDefault(clientId);
            if (ids is null)
                return null;

            var index = customIndexCollection.Find(x => x.Id == customIndexUpdated.Id);
            customIndexCollection.Remove(index);

            customIndexCollection.Add(customIndexUpdated);
            return customIndexUpdated;
        }

        public bool RemoveIndex(Guid userId, string id)
        {
            var ids = userIndicesCollection.GetValueOrDefault(userId);
            if (ids is null|| !ids.Contains(id))
                return false;

            customIndexCollection.RemoveAll(x => x.Id == id);

            return true;
        }

        Task<ActionResult<CustomIndexResponse>> ICustomIndexService.GetIndex(Guid userId, string indexId)
        {
            throw new NotImplementedException();
        }

        Task<ActionResult<IEnumerable<CustomIndexResponse>>> ICustomIndexService.GetAllForUser(Guid userid)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> CreateIndex(Guid userId, CustomIndexResponse customIndex)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateIndex(Guid userId, CustomIndexResponse customIndexUpdated)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> ICustomIndexService.RemoveIndex(Guid userId, string id)
        {
            throw new NotImplementedException();
        }
    }
}
