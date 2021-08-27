﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("createAchRelationship")]
        public IActionResult CreateAchRelationship(CreateAchRelationshipRequest request)
        {
			return accountservice.CreateAchRelationship(request);
        }
	}
}
