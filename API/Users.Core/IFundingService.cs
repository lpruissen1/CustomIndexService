using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core.Request;

namespace Users.Core
{
	public interface IFundingService
	{
		public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request);
		public FundingRequestStatusValue TransferFunds(Guid userId, FundAccountRequest request);
		public IActionResult GetAchRelationships(Guid userId);
		public IActionResult CancelTransfer(Guid userId, Guid transferId);
		public IActionResult GetTransfers(Guid userId);
	}
}
