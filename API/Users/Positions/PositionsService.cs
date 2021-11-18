using AlpacaApiClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Users.Core;
using Users.Core.Response.Positions;
using Users.CustomIndices;
using Users.Database.Repositories.Interfaces;

namespace Users.Positions
{
	public class PositionsService : IPositionsService
	{
		public PositionsService(IUserPositionsRepository userPositionsRepository, IUserAccountsRepository userAccountsRepository, ICustomIndexService customIndexService, ILogger logger)
		{
			this.userAccountsRepository = userAccountsRepository;
			this.customIndexService = customIndexService;
			this.userPositionsRepository = userPositionsRepository;
			alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" }, logger);
		}

		private IUserAccountsRepository userAccountsRepository { get; }
		private ICustomIndexService customIndexService { get; }
		private IUserPositionsRepository userPositionsRepository { get; }
		private AlpacaClient alpacaClient { get; }

		public IActionResult GetAllPositions(Guid userId)
		{
			var account = userAccountsRepository.GetByUserId(userId);

			var remotePosition = alpacaClient.GetAllPositions(account.Accounts[0].AccountId);

			var positions = userPositionsRepository.GetByUserId(userId);

			var response = PositionsMapper.MapPositions(remotePosition, positions);

			return new OkObjectResult(response);

		}

		public IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId)
		{
			var account = userAccountsRepository.GetByUserId(userId);

			var remotePosition = alpacaClient.GetAllPositions(account.Accounts[0].AccountId);

			var positions = userPositionsRepository.GetByUserId(userId);

			positions.Positions.RemoveAll(x => x.Portfolios.ContainsKey(portfolioId.ToString()));

			var response = PositionsMapper.MapPositions(remotePosition, positions);

			return new OkObjectResult(response);
		}

		public async Task<PositionsResponse> GetPortfoliosByPortfolio(Guid userId)
		{
			var account = userAccountsRepository.GetByUserId(userId);
			var indices = await customIndexService.GetAllForUserNew(userId.ToString());
			var remotePosition = alpacaClient.GetAllPositions(account.Accounts[0].AccountId);

			var positions = userPositionsRepository.GetByUserId(userId);

			var response = PositionsMapper.MapPositions(remotePosition, positions, indices);

			return response;
		}
	}
}
