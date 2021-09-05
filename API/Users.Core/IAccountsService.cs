using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;

namespace Users.Core
{
	public interface IAccountsService
	{
		public IActionResult CreateTradingAccount(CreateAccountRequest request);
		public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request);
		public IActionResult TransferFunds(Guid userId, FundAccountRequest request);
		public IActionResult GetAchRelationships(Guid userId);
		public IActionResult ExecuteBulkTrade(Guid userId, BulkPurchaseRequest request);

	}
}
