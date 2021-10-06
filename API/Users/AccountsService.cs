﻿using AlpacaApiClient;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
		public AccountsService(IUserRepository userRepository, IUserPositionsRepository userPositionsRepository, IUserTransfersRepository userTransfersRepository, IUserAccountsRepository userAccountsRepository, IUserDisclosuresRepository userDiclosuresRepository, IUserDocumentsRepository userDocumentsRepository, IUserOrdersRepository userOrdersRepository, IPositionAdditionHandler positionAdditionHandler, ILogger logger)
		{
			this.userRepository = userRepository;
			this.userAccountsRepository = userAccountsRepository;
			this.userTransfersRepository = userTransfersRepository;
			this.userDiclosuresRepository = userDiclosuresRepository;
			this.userDocumentsRepository = userDocumentsRepository;
			this.userOrdersRepository = userOrdersRepository;
			this.positionAdditionHandler = positionAdditionHandler;
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
		private IPositionAdditionHandler positionAdditionHandler { get; }
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
				userTransfersRepository.Create(new UserTransfers { UserId = new Guid(request.UserId)});
				userPositionsRepository.Create(new UserPositions { UserId = new Guid(request.UserId)});
				return new OkResult();
			}

			return new BadRequestResult();
		}

		public IActionResult ExecuteBulkPurchase(Guid userId, BulkPurchaseRequest request)
		{
			var transationId = Guid.NewGuid();
			var alpacaRequests = AlpacaAccountRequestMapper.MapBulkPurchaseOrder(request);
			var alpacaAccount = userAccountsRepository.GetByUserId(userId).Accounts.First().AccountId;
			var orders = new List<Order>();

			foreach (var alpacaRequest in alpacaRequests)
			{
				var alpacaOrderResponse = alpacaClient.ExecuteOrder(alpacaRequest, alpacaAccount);

				if (alpacaOrderResponse is not null)
				{
					if (alpacaOrderResponse.status == OrderStatusValue.filled)
					{
						var newPosition = new Position(alpacaOrderResponse.symbol, alpacaOrderResponse.filled_avg_price.Value, request.PortfolioId, alpacaOrderResponse.filled_qty);
						positionAdditionHandler.AddPosition(userId, newPosition);
					}

					orders.Add(AlpacaResponseMapper.MapAlpacaOrderResponse(alpacaOrderResponse, transationId, request.PortfolioId));
				}
			}

			userOrdersRepository.AddOrders(userId, orders);

			return new OkResult();
		}

		public IActionResult GetOrders(Guid userId)
		{
			var userOrders = userOrdersRepository.GetByUserId(userId).Orders;

			if (userOrders is not null)
				return new OkObjectResult(userOrders);

			return new BadRequestResult();
		}
	}
}
