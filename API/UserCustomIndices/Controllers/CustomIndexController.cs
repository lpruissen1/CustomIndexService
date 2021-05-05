﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UserCustomIndices.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UserCustomIndices.Model.Response;
using UserCustomIndices.Core.Model.Requests;

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
        public Task<ActionResult<IEnumerable<CustomIndexRequest>>> Get(Guid userId)
        {
            return indexService.GetAllForUser(userId);
        }

        [HttpGet("{indexId:length(24)}", Name = "GetCustomIndex")]
        public Task<ActionResult<CustomIndexRequest>> GetById(Guid userId, string indexId)
        {

            return indexService.GetIndex(userId, indexId);
        }

        [HttpPost]
        public IActionResult Create(Guid userId, CustomIndexRequest index)
        {
            return indexService.CreateIndex(userId, index);
        }

        [HttpPut("{userId:length(24)}")]
        public Task<IActionResult> Update(Guid userId, CustomIndexRequest updatedIndex)
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
