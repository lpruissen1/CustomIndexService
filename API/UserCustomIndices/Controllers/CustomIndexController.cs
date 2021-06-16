using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Model.Response;
using UserCustomIndices.Services;

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
        public Task<ActionResult<IEnumerable<CustomIndexResponse>>> Get(string userId)
        {
            return indexService.GetAllForUser(userId);
        }

        [HttpGet("GetCustomIndex")]
        public Task<ActionResult<CustomIndexResponse>> GetById(string userId, string indexId)
        {
            return indexService.GetIndex(userId, indexId);
        }

        [HttpPost]
        public IActionResult Create(string userId, CreateCustomIndexRequest index)
        {
            return indexService.CreateIndex(userId, index);
        }

        [HttpPut("UpdateIndex")]
        public IActionResult Update(string userId, CustomIndexRequest updatedIndex)
        {
            return indexService.UpdateIndex(userId, updatedIndex);
        }

        [HttpDelete("DeleteIndex")]
        public IActionResult Delete(string userId, string indexId)
        {
            return indexService.RemoveIndex(userId, indexId);    
        }
    }
}
