﻿using Microsoft.AspNetCore.Mvc;
using Users.Core.Request;

namespace Users.Core
{
	public interface IAccountsService
	{
		public IActionResult CreateTradingAccount(CreateAccountRequest request);
		public IActionResult CreateAchRelationship(CreateAchRelationshipRequest request);
		public IActionResult TransferFunds(FundAccountRequest request);
		public IActionResult GetAchRelationships(string accountId);
	}
}
