using AlpacaApiClient;
using Core;
using Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		public AccountsService(IUserRepository userRepository, IUserPositionsRepository userPositionsRepository, IUserTransfersRepository userTransfersRepository, IUserAccountsRepository userAccountsRepository, IUserDisclosuresRepository userDiclosuresRepository, IUserDocumentsRepository userDocumentsRepository, IUserOrdersRepository userOrdersRepository, ILogger logger)
		{
			this.userRepository = userRepository;
			this.userAccountsRepository = userAccountsRepository;
			this.userTransfersRepository = userTransfersRepository;
			this.userDiclosuresRepository = userDiclosuresRepository;
			this.userDocumentsRepository = userDocumentsRepository;
			this.userOrdersRepository = userOrdersRepository;
			this.userPositionsRepository = userPositionsRepository;
			this.alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" }, logger);
		}

		private IUserRepository userRepository { get; }
		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserTransfersRepository userTransfersRepository { get; }
		private IUserDisclosuresRepository userDiclosuresRepository { get; }
		private IUserDocumentsRepository userDocumentsRepository { get; }
		private IUserOrdersRepository userOrdersRepository { get; }
		private IUserPositionsRepository userPositionsRepository { get; }
		private AlpacaClient alpacaClient { get; }

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
				userTransfersRepository.Create(new UserTransfers(new Guid(request.UserId)));
				userPositionsRepository.Create(new UserPositions(new Guid(request.UserId)));
				userOrdersRepository.Create(new UserOrders(new Guid(request.UserId)));

				return new OkResult();
			}

			return new BadRequestResult();
		}

		public AccountHistoryResponse GetAccountHistory(Guid userId, TimePeriod timePeriod)
		{
			var accountId = userAccountsRepository.GetByUserId(userId).Accounts[0].AccountId;
			var alpacaAccountHistoryResponse = alpacaClient.AccountHistory(accountId, timePeriod);
			var accountTransfers = userTransfersRepository.GetByUserId(userId);

			var relevantTransfers = GetTransfersForTimePeriod(accountTransfers, timePeriod);

			if (alpacaAccountHistoryResponse.Code == 200)
				return ResponseMapper.MapAlpacaAccountHistoryResponse(alpacaAccountHistoryResponse, relevantTransfers, accountTransfers.Transfers.Min(x => x.Created));

			return default;
		}

		public List<Transfer> GetTransfersForTimePeriod(UserTransfers transfers, TimePeriod timePeriod)
		{
			if (timePeriod == TimePeriod.AllTime)
				return transfers.Transfers;

			var startDate = DateTime.UtcNow.AddMonths(-(timePeriod.GetMonthsFromTimePeriod()));
			List<Transfer> relevantTransfers = new List<Transfer>();

			foreach(var transfer in transfers.Transfers)
			{
				if (transfer.Status == TransferStatusValue.COMPLETE && transfer.Created >= startDate)
					relevantTransfers.Add(transfer);
			}

			return relevantTransfers;
		}

		public IActionResult ExecuteBulkOrder(Guid userId, BulkOrderRequest request)
		{
			var transationId = Guid.NewGuid();
			var alpacaRequests = AlpacaAccountRequestMapper.MapBulkRequestOrder(request);
			var alpacaAccount = userAccountsRepository.GetByUserId(userId).Accounts.First().AccountId;

			foreach (var alpacaRequest in alpacaRequests)
			{
				var alpacaOrderResponse = alpacaClient.ExecuteOrder(alpacaRequest, alpacaAccount);

				if (alpacaOrderResponse is not null)
					userOrdersRepository.AddOrder(userId, AlpacaResponseMapper.MapAlpacaOrderResponse(alpacaOrderResponse, transationId, request.PortfolioId));
			}

			return new OkResult();
		}
	}
}
