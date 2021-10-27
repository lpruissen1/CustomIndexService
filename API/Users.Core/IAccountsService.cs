using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;

namespace Users.Core
{
	public interface IAccountsService
	{
		public IActionResult CreateTradingAccount(CreateAccountRequest request);
		public IActionResult ExecuteBulkPurchase(Guid userId, BulkOrderRequest request);
	}
}
