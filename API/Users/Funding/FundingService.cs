using AlpacaApiClient;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Users.Core;
using Users.Core.Request;
using Users.Core.Response;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;
using Users.Mappers;

namespace Users.Funding
{
	public class FundingService : IFundingService
	{
		public FundingService(IUserAccountsRepository userAccountsRepository, IUserTransfersRepository userTransferRepository, ILogger logger)
		{
			this.userAccountsRepository = userAccountsRepository;
			this.userTransferRepository = userTransferRepository;
			alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" }, logger);
		}

		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserTransfersRepository userTransferRepository { get; }
		private AlpacaClient alpacaClient { get; }

		public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request)
		{
			var alpacaAccount = userAccountsRepository.GetByUserId(userId);
			var alpacaRequest = AlpacaAccountRequestMapper.MapCreateAchRelationshipRequest(request);

			var alpacaCreateAccountResponse = alpacaClient.CreateAchRelationsip(alpacaRequest, alpacaAccount.Accounts.First().AccountId);

			if(alpacaCreateAccountResponse is not null)
			{

				alpacaAccount.Accounts.First().AchRelationship = new AchRelationship { Id = alpacaCreateAccountResponse.id, Nickname = alpacaCreateAccountResponse.nickname, Status = alpacaCreateAccountResponse.status };

				userAccountsRepository.Update(alpacaAccount);

				return new OkObjectResult(new CreateAchRelationshipResponse() { Status = alpacaCreateAccountResponse.status.ToString(), Nickname = alpacaCreateAccountResponse.nickname });
			}

			return new BadRequestResult();
		}

		public IActionResult GetAchRelationships(Guid userId)
		{
			var achRelationship = userAccountsRepository.GetByUserId(userId).Accounts.FirstOrDefault()?.AchRelationship ?? null;

			return achRelationship is not null ? new OkObjectResult(new GetAchRelationshipResponse { Nickname = achRelationship.Nickname, RelationshipId = achRelationship.Id, Status = achRelationship.Status.ToString()}) : new OkObjectResult(new GetAchRelationshipResponse());
		}

		public IActionResult TransferFunds(Guid userId, FundAccountRequest request)
		{
			var alpacaAccount = userAccountsRepository.GetByUserId(userId).Accounts.First();

			if (alpacaAccount.AchRelationship.Id != request.RelationshipId)
				return new BadRequestResult();

			var alpacaRequest = AlpacaAccountRequestMapper.MapTransferRequest(request);
			var alpacaTransferResponse = alpacaClient.TransferFunds(alpacaRequest, alpacaAccount.AccountId);

			if (alpacaTransferResponse is not null)
				userTransferRepository.AddTransfer(userId, AlpacaResponseMapper.MapAlpacaTransferResponse(alpacaTransferResponse));

			return alpacaTransferResponse is not null ? new OkObjectResult(new CreateFundingTransferResponse() { success = true }) : new OkObjectResult(new CreateFundingTransferResponse() { success = false });
		}

		public IActionResult CancelTransfer(Guid userId, Guid transferId)
		{
			var transfer = userTransferRepository.GetByUserId(userId).Transfers.FirstOrDefault(x => x.TransferId == transferId);

			if (transfer is null)
				return new BadRequestResult();

			var response = alpacaClient.CancelTransfer(transfer.AccountId, transferId);

			if (response)
			{
				transfer.Status = TransferStatusValue.CANCELLED;
				userTransferRepository.UpdateTransfer(userId, transfer);
				return new OkResult();
			}

			return new BadRequestResult();
		}

		public IActionResult GetTransfers(Guid userId)
		{
			var transfers = userTransferRepository.GetByUserId(userId);

			if (transfers is null)
				return new BadRequestResult();

			var responseTransfers = transfers.Transfers.Select(x => FundingMapper.MapTransfer(x)).ToList();
			return new OkObjectResult(responseTransfers);
		}
	}
}
