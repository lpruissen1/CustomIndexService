﻿using AlpacaApiClient;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Users.Core;
using Users.Core.Request;
using Users.Core.Response;
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

			return achRelationship is not null ? new OkObjectResult(new GetAchRelationshipResponse { Nickname = achRelationship.Nickname, Status = achRelationship.Status.ToString()}) : new OkObjectResult(new GetAchRelationshipResponse());
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