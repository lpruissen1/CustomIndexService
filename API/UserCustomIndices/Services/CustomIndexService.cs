using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Mappers;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Services
{
	public class CustomIndexService : ICustomIndexService
    {
        private readonly IIndicesRepository indicesRepository;
        private readonly IResponseMapper responseMapper;
        private readonly IRequestMapper requestMapper;

        public CustomIndexService(IIndicesRepository indicesRepository, IRequestMapper requesteMapper, IResponseMapper responseMapper)
        {
            this.indicesRepository = indicesRepository;
            this.requestMapper = requesteMapper;
            this.responseMapper = responseMapper;
        }

        public async Task<ActionResult<CustomIndexResponse>> GetIndex(string userId, string indexId)
        {
            var index = await indicesRepository.Get(userId, indexId);

            return new ActionResult<CustomIndexResponse>(responseMapper.Map(index));
        }

        public async Task<ActionResult<IEnumerable<CustomIndexResponse>>> GetAllForUser(string userid)
        {
            var result = await indicesRepository.GetAllForUser(userid);

            return new ActionResult<IEnumerable<CustomIndexResponse>>(result.Select(index => responseMapper.Map(index)));
        }

        public IActionResult CreateIndex(string userId, CreateCustomIndexRequest customIndex)
        {
            indicesRepository.Create(requestMapper.Map(customIndex));

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
