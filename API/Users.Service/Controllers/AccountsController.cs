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

        [HttpPost("create-ach-relationship/{userId}")]
        public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request)
        {
			return accountservice.CreateAchRelationship(userId, request);
        }

		[HttpPost("transferFunds")]
		public IActionResult TransferFunds(FundAccountRequest request)
		{
			return accountservice.TransferFunds(request);
		}

		[HttpGet("get-ach-relationship/{userId}")]
		public IActionResult GetAchRelationship(Guid userId) 
		{
			return accountservice.GetAchRelationships(userId);
		}
	}
}
