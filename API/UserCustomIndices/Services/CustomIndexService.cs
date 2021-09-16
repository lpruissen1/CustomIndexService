﻿using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Mappers;
using UserCustomIndices.Model.Response;
using UserCustomIndices.Validators;

namespace UserCustomIndices.Services
{
	public class CustomIndexService : ICustomIndexService
    {
        private readonly IIndicesRepository indicesRepository;
        private readonly IResponseMapper responseMapper;
		private readonly ICustomIndexValidator customIndexValidator;
		private readonly ILogger logger;
		private readonly IRequestMapper requestMapper;

        public CustomIndexService(IIndicesRepository indicesRepository, IRequestMapper requestMapper, IResponseMapper responseMapper, ICustomIndexValidator customIndexValidator, ILogger logger)
        {
            this.indicesRepository = indicesRepository;
            this.requestMapper = requestMapper;
            this.responseMapper = responseMapper;
			this.customIndexValidator = customIndexValidator;
			this.logger = logger;
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

        public IActionResult CreateIndex(string userId, CreateCustomIndexRequest customIndexRequest)
        {
			var customIndex = requestMapper.Map(customIndexRequest);

			var valid = customIndexValidator.Validate(customIndex);

			if (valid)
			{
				indicesRepository.Create(customIndex);
				logger.LogInformation(new EventId(1), $"Portfolio, {customIndex.IndexId}, created for user {userId}");
				return new OkResult();
			}

			logger.LogInformation(new EventId(1), $"Portfolio, {customIndex.IndexId}, failed to be created for user {userId}");

			return new BadRequestResult(); 
        }

        public IActionResult UpdateIndex(string userId, CustomIndexRequest customIndexUpdated)
        {
			var success = indicesRepository.UpdateIndex(userId, requestMapper.Map(customIndexUpdated));

			if (success)
			{
				logger.LogInformation(new EventId(1), $"Portfolio, {customIndexUpdated.IndexId} for user, {userId}, was updated");
				return new OkResult();
			}

			logger.LogInformation(new EventId(1), $"Portfolio, {customIndexUpdated.IndexId} for user {userId} failed to update");
			return new BadRequestResult();
		}

        public IActionResult RemoveIndex(string userId, string indexId)
        {
			var success = indicesRepository.DeleteIndex(userId, indexId);

			if (success)
			{
				logger.LogInformation(new EventId(1), $"Portfolio, {indexId} for user {userId}");
				return new OkResult();
			}


			logger.LogInformation(new EventId(1), $"Portfolio, {indexId} for user, {userId}, was not deleted properly");
			return new BadRequestResult();
		}
    }
}
