using DB = Database.Model.User.CustomIndices;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API = UserCustomIndices.Model.Response;
using UserCustomIndices.Model.Response;
using Database.Model.User.CustomIndices;

// convert the fomr API -> DB models
// This is where I want validation to take place
namespace UserCustomIndices.Services
{
    public class CustomIndexService : ICustomIndexService
    {
        private readonly IIndicesRepository indicesRepository;

        public CustomIndexService(IIndicesRepository indicesRepository)
        {
            this.indicesRepository = indicesRepository;
        }

        public async Task<ActionResult<CustomIndexResponse>> GetIndex(Guid userId, string indexId)
        {
            var index = await indicesRepository.Get(userId, indexId);


            return new ActionResult<CustomIndexResponse>(new CustomIndexResponse
            { 
                Id = index.Id,
                Markets = new API.ComposedMarkets { Markets = index.Markets.Markets },
                Test = index.Test
            });
        }

        public async Task<ActionResult<IEnumerable<CustomIndexResponse>>> GetAllForUser(Guid userid)
        {
            var result = await indicesRepository.GetAllForUser(userid);

            return new ActionResult<IEnumerable<CustomIndexResponse>>(result.Select(index => new CustomIndexResponse
            {
                Id = index.Id,
                Markets = new API.ComposedMarkets { Markets = index.Markets?.Markets},
                Test = index.Test
            }));
        }

        public async Task<IActionResult> CreateIndex(Guid userId, CustomIndexResponse customIndex)
        {
            var insert = indicesRepository.Create( new CustomIndex 
            {
                UserId = userId.ToString(),
                Test = customIndex.Test,
                Markets = new DB.ComposedMarkets { Markets = customIndex.Markets.Markets }
            });

            insert.Wait();

            return insert.IsCompletedSuccessfully ? new OkResult() : new BadRequestResult(); 
        }

        Task<IActionResult> ICustomIndexService.UpdateIndex(Guid userId, CustomIndexResponse customIndexUpdated)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> ICustomIndexService.RemoveIndex(Guid userId, string id)
        {
            throw new NotImplementedException();
        }
    }
}