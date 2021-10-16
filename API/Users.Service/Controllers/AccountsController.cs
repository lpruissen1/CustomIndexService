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

        [HttpGet("accountHistory/{userId}")]
        public IActionResult AccountHistory(Guid userId)
        {
			var response = accountservice.GetAccountHistory(userId);

			if (response is not null)
				return new OkObjectResult(response);

			return new BadRequestResult();
        }

		[HttpPost("execute-bulk-market-order/{userId}")]
		public IActionResult ExecuteBulkMarketOrder(Guid userId, BulkPurchaseRequest request) 
		{
			return accountservice.ExecuteBulkPurchase(userId, request);
		}
	}
}
