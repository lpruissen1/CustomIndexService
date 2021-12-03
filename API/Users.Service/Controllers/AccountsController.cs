using Core;
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

        [HttpGet("accountHistory/{userId}/{timePeriod}")]
        public IActionResult AccountHistory(Guid userId, TimePeriod timePeriod)
        {
			var response = accountservice.GetAccountHistory(userId, timePeriod);

			if (response is not null)
				return new OkObjectResult(response);

			return new BadRequestResult();
        }

		[HttpPost("execute-bulk-market-order/{userId}")]
		public IActionResult ExecuteBulkMarketOrder(Guid userId, BulkOrderRequest request) 
		{
			return accountservice.ExecuteBulkOrder(userId, request);
		}

		[HttpPost("execute-individual-market-order/{userId}")]
		public IActionResult ExecuteIndividualMarketOrder(Guid userId, IndividualOrderRequest request)
		{
			return accountservice.ExecuteIndividualOrder(userId, request);
		}
	}
}
