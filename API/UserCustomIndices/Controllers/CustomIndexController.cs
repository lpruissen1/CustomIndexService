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
        public IActionResult Get(Guid userId)
        {
            var indices = IndexService.Get(userId);

            if(indices is null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(indices);
        }

        [HttpGet("{indexId:length(24)}", Name = "GetCustomIndex")]
        public IActionResult Get(Guid userId, string indexId)
        {
            if (indexId.Length != 24)
                return BadRequest();

            var index = IndexService.Get(indexId);

            if (index is null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(index);
        }

        [HttpPost]
        public ActionResult<CustomIndex> Create(CustomIndex index)
        {
            if (!IndexValidator.Validate(index))
                return BadRequest();

            IndexService.Create(index);
            return Created("Create", index);
        }

        [HttpPut("{userId:length(24)}")]
        public IActionResult Update(Guid userId, CustomIndex updatedIndex)
        {
            if (!IndexValidator.Validate(updatedIndex))
                return new BadRequestResult();

            var index = IndexService.Update(userId, updatedIndex);
            if (index is null)
                return new NotFoundResult();

            return new OkObjectResult(index);
        }

        [HttpDelete("{clientId:length(24)}")]
        public IActionResult Delete(Guid userId, string indexId)
        {
            if (indexId.Length != 24)
                return BadRequest();

            var index = IndexService.Get(indexId);

            if (index is null)
            {
                return NotFound();
            }

            IndexService.Remove(index.Id);

            return NoContent();
        }
    }
}
