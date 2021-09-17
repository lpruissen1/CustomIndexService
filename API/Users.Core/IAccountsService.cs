using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;

namespace Users.Core
{
	public interface IAccountsService
	{
		public IActionResult CreateTradingAccount(CreateAccountRequest request);
		public IActionResult ExecuteBulkTrade(Guid userId, BulkPurchaseRequest request);
		public IActionResult GetOrders(Guid userId);
	}
}
