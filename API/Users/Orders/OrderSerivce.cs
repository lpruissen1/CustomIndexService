using AlpacaApiClient;
using Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Users.Core;
using Users.Core.Response.Orders;
using Users.Database.Repositories.Interfaces;

namespace Users.Orders
{
	public class OrderSerivce : IOrderService
	{

		public OrderSerivce(IUserOrdersRepository userOrdersRepository, IUserAccountsRepository userAccountsRepository, ILogger logger)
		{
			this.userOrdersRepository = userOrdersRepository; 
			this.userAccountsRepository = userAccountsRepository; 
			this.alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" }, logger);
		}

		public IUserOrdersRepository userOrdersRepository { get; }
		public IUserAccountsRepository userAccountsRepository { get; }
		private AlpacaClient alpacaClient { get; }

		public OrderResponse GetOrders(Guid userId)
		{
			var userOrders = userOrdersRepository.GetByUserId(userId);
			
			if (userOrders is not null)
			{
				var orderResponse = OrderMapper.MapOrders(userOrders.Orders);
				return orderResponse;
			}

			return default;
		}

		public bool CancelOrder(Guid userId, Guid orderId)
		{
			var order = userOrdersRepository.GetByUserId(userId).Orders.FirstOrDefault(x => x.OrderId == orderId);

			if (order is null)
				return false;

			var accountId = userAccountsRepository.GetByUserId(userId).Accounts[0].AccountId;
			var response = alpacaClient.CancelOrder(accountId, orderId);

			if (response)
			{
				order.Status = OrderStatusValue.canceled;
				userOrdersRepository.UpdateOrder(userId, order);
				return true;
			}

			return false;
		}
	}
}
