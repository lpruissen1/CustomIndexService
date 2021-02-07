using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using UserCustomIndices.Services;
using Database.Model.User.CustomIndices;
using System;
using UserCustomIndices.Validators;

namespace UserCustomIndices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomIndexController : ControllerBase
    {
        private readonly ILogger<CustomIndexController> Logger;
        private readonly ICustomIndexService IndexService;
        private readonly ICustomIndexValidator IndexValidator;

        public CustomIndexController(ICustomIndexService indexService, ICustomIndexValidator customIndexValidator)
        {
            IndexService = indexService;
            IndexValidator = customIndexValidator;
        }

        [HttpGet]
        public ActionResult<List<CustomIndex>> Get(Guid userId)
        {
            var indices = IndexService.Get(userId);

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

            var index = IndexService.Get(indexId);

            if (index is null)
            {
                return new NotFoundResult();
            }

            return index;
        }

        [HttpPost]
        public ActionResult<CustomIndex> Create(CustomIndex index)
        {
            if (!IndexValidator.Validate(index))
                return BadRequest();

            IndexService.Create(index);
            return Created("Create", index);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, CustomIndex updatedIndex)
        {
            var index = IndexService.Get(id);

            if (index is null)
            {
                return NotFound();
            }

            IndexService.Update(id, updatedIndex);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var index = IndexService.Get(id);

            if (index is null)
            {
                return NotFound();
            }

            IndexService.Remove(index.Id);

            return NoContent();
        }
    }
}
