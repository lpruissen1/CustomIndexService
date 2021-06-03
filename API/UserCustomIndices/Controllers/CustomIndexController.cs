﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{indexId:length(24)}", Name = "GetCustomIndex")]
        public Task<ActionResult<CustomIndexResponse>> GetById(Guid userId, string indexId)
        {

            return indexService.GetIndex(userId, indexId);
        }

        [HttpPost]
        public IActionResult Create(string userId, CreateCustomIndexRequest index)
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
