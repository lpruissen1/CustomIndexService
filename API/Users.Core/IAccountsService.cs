using Core;
using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;
using Users.Core.Response;

namespace Users.Core
{
	public interface IAccountsService
	{
		IActionResult CreateTradingAccount(CreateAccountRequest request);
		IActionResult ExecuteBulkPurchase(Guid userId, BulkPurchaseRequest request);
		AccountHistoryResponse GetAccountHistory(Guid userId, TimePeriod timePeriod);
	}
}
