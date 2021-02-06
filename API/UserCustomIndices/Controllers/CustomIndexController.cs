using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UserCustomIndices.Services;
using Database.Model.User.CustomIndices;
using System;

namespace UserCustomIndices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomIndexController : ControllerBase
    {
        private readonly ILogger<CustomIndexController> Logger;
        private readonly ICustomIndexService indexService;

        public CustomIndexController(ICustomIndexService bookService)
        {
            indexService = bookService;
        }

        [HttpGet]
        public ActionResult<List<CustomIndex>> Get(Guid userId)
        {
            var indices = indexService.Get(userId);

            if(indices is null)
            {
                return new NotFoundResult();
            }

            return indices;
        }

        [HttpGet("{id:length(24)}", Name = "GetCustomIndex")]
        public ActionResult<CustomIndex> Get(string indexId)
        {
            if (indexId.Length != 24)
                return BadRequest();

            var index = indexService.Get(indexId);

            if (index is null)
            {
                return new NotFoundResult();
            }

            return index;
        }

        [HttpPost]
        public ActionResult<CustomIndex> Create(CustomIndex index)
        {
            indexService.Create(index);

            return CreatedAtRoute("GetBook", new { id = index.Id.ToString() }, index);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CustomIndex updatedIndex)
        {
            var index = indexService.Get(id);

            if (index is null)
            {
                return NotFound();
            }

            indexService.Update(id, updatedIndex);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var index = indexService.Get(id);

            if (index is null)
            {
                return NotFound();
            }

            indexService.Remove(index.Id);

            return NoContent();
        }
    }
}
