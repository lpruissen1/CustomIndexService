using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core;
using Users.Core.Request;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class AccountsController : ControllerBase
    {
		private readonly IAccountsService accountservice;

		public AccountsController(IAccountsService accountservice)
        {
			this.accountservice = accountservice;
		}

        [HttpPost("create")]
        public IActionResult Create(CreateAccountRequest request)
        {
			return accountservice.CreateTradingAccount(request);
        }

		[HttpPost("execute-bulk-market-order/{userId}")]
		public IActionResult ExecuteBulkMarketOrder(Guid userId, BulkPurchaseRequest request) 
		{
			return accountservice.ExecuteBulkTrade(userId, request);
		}

		[HttpGet("get-orders/{userId}")]
		public IActionResult GetOrders(Guid userId) 
		{
			return accountservice.GetOrders(userId);
		}
	}
}
