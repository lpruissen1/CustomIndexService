using Core;
using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;
using Users.Core.Response;

namespace Users.Core
{
	public interface IAccountsService
	{
		public IActionResult ExecuteBulkOrder(Guid userId, BulkOrderRequest request);
		public IActionResult ExecuteIndividualOrder(Guid userId, IndividualOrderRequest request);
		IActionResult CreateTradingAccount(CreateAccountRequest request);
		AccountHistoryResponse GetAccountHistory(Guid userId, TimePeriod timePeriod);
	}
}
