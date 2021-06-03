using AutoMapper;
using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Mappers;

// convert the fomr API -> DB models
// This is where I want validation to take place
namespace UserCustomIndices.Services
{
    public class CustomIndexService : ICustomIndexService
    {
        private readonly IIndicesRepository indicesRepository;
        private readonly IMapper mapper;
        private readonly IRequestMapper responseMapper;

        public CustomIndexService(IIndicesRepository indicesRepository, IMapper mapper, IRequestMapper responseMapper)
        {
            this.indicesRepository = indicesRepository;
            this.mapper = mapper;
            this.responseMapper = responseMapper;
        }

        public async Task<ActionResult<CustomIndexRequest>> GetIndex(Guid userId, string indexId)
        {
            var index = await indicesRepository.Get(userId, indexId);


            return new ActionResult<CustomIndexRequest>(CustomIndexRequestMapper.MapToCustomIndexRequest(index));
        }

        public async Task<ActionResult<IEnumerable<CustomIndexRequest>>> GetAllForUser(string userid)
        {
            var result = await indicesRepository.GetAllForUser(userid);

            return new ActionResult<IEnumerable<CustomIndexRequest>>(result.Select(index => CustomIndexRequestMapper.MapToCustomIndexRequest(index)));
        }

        public IActionResult CreateIndex(string userId, CustomIndexRequest customIndex)
        {
            indicesRepository.Create(responseMapper.Map(customIndex));

            return new OkResult(); 
        }

        Task<IActionResult> ICustomIndexService.UpdateIndex(Guid userId, CustomIndexRequest customIndexUpdated)
        {
            throw new NotImplementedException();
        }

        Task<IActionResult> ICustomIndexService.RemoveIndex(Guid userId, string id)
        {
            throw new NotImplementedException();
        }
    }
}
