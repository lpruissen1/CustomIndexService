using AlpacaApiClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Users.Core;
using Users.Core.Request;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;
using Users.Mappers;

namespace Users
{
	public class AccountsService : IAccountsService
	{
		public AccountsService(IUserRepository userRepository, IUserAccountsRepository userAccountsRepository, IUserDisclosuresRepository userDiclosuresRepository, IUserDocumentsRepository userDocumentsRepository)
		{
			this.userRepository = userRepository;
			this.userAccountsRepository = userAccountsRepository;
			this.userDiclosuresRepository = userDiclosuresRepository;
			this.userDocumentsRepository = userDocumentsRepository;
			this.alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" });
		}

		private IUserRepository userRepository { get; }
		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserDisclosuresRepository userDiclosuresRepository { get; }
		private IUserDocumentsRepository userDocumentsRepository { get; }
		private AlpacaClient alpacaClient { get; }

		public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request)
		{
			var alpacaRequest = AlpacaAccountRequestMapper.MapCreateAchRelationshipRequest(request);
			var alpacaCreateAccountResponse = alpacaClient.CreateAchRelationsip(alpacaRequest, request.AlpacaAccountId);

			if(alpacaCreateAccountResponse is not null)
			{
				var user = userAccountsRepository.GetByUserId(userId);
				var account = user.Accounts.First(account => account.AccountId.ToString() == request.AlpacaAccountId);

				account.AchRelationship = new AchRelationship { Id = alpacaCreateAccountResponse.id, Status = alpacaCreateAccountResponse.status };

				userAccountsRepository.Update(user);

				return new OkResult();
			}

			return new BadRequestResult();
		}

		public IActionResult GetAchRelationships(string accountId)
		{
			// get our internal info, not from alpaca
			var getAchResponse = alpacaClient.GetAchRelationships(accountId);

			
			return null;
			//return getAchResponse ? new OkResult() : new BadRequestResult();
		}

		public IActionResult TransferFunds(FundAccountRequest request)
		{
			var alpacaRequest = AlpacaAccountRequestMapper.MapTransferRequest(request);
			var alpacaTransferResponse = alpacaClient.TransferFunds(alpacaRequest, request.AlpacaAccountId);

			return alpacaTransferResponse is not null ? new OkResult() : new BadRequestResult();
		}

		public IActionResult CreateTradingAccount(CreateAccountRequest request)
		{
			var alpacaRequest = AlpacaAccountRequestMapper.MapCreateAccountRequest(request);
			var alpacaCreateAccountResponse = alpacaClient.CreateAccount(alpacaRequest);

			if (alpacaCreateAccountResponse is not null)
			{
				var user = userRepository.GetByUserId(request.UserId);

				userRepository.Update(CreateAccountRequestDbMapper.MapToUser(user, request));
				userAccountsRepository.Create(CreateAccountRequestDbMapper.MapUserAccounts(request, alpacaCreateAccountResponse));
				userDiclosuresRepository.Create(CreateAccountRequestDbMapper.MapUserDisclosures(request));
				userDocumentsRepository.Create(CreateAccountRequestDbMapper.MapUserDocuments(request));

				return new OkResult();
			}

			return new BadRequestResult();
		}
	}
}
