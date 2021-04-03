using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserCustomIndices.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomIndexController : ControllerBase
    {
        private readonly ICustomIndexService indexService;

        public CustomIndexController(ICustomIndexService indexService)
        {
            this.indexService = indexService;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<CustomIndexResponse>>> Get(Guid userId)
        {
            return indexService.GetAllForUser(userId);
        }

        [HttpGet("{indexId:length(24)}", Name = "GetCustomIndex")]
        public Task<ActionResult<CustomIndexResponse>> GetById(Guid userId, string indexId)
        {

            return indexService.GetIndex(userId, indexId);
        }

        [HttpPost]
        public Task<IActionResult> Create(Guid userId, CustomIndexResponse index)
        {
            return indexService.CreateIndex(userId, index);
        }

        [HttpPut("{userId:length(24)}")]
        public Task<IActionResult> Update(Guid userId, CustomIndexResponse updatedIndex)
        {
            return indexService.UpdateIndex(userId, updatedIndex);
        }

        [HttpDelete("{clientId:length(24)}")]
        public Task<IActionResult> Delete(Guid userId, string indexId)
        {
            return indexService.RemoveIndex(userId, indexId);    
        }
    }
}
