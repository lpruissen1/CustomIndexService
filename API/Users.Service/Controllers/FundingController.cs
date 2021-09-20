using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core;
using Users.Core.Request;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
	public class FundingController : ControllerBase
    {
		private readonly IFundingService fundingService;

		public FundingController(IFundingService fundingService)
        {
			this.fundingService = fundingService;
		}

        [HttpPost("create-ach-relationship/{userId}")]
        public IActionResult CreateAchRelationship(Guid userId, CreateAchRelationshipRequest request)
        {
			return fundingService.CreateAchRelationship(userId, request);
        }

		[HttpPost("transfer-funds/{userId}")]
		public IActionResult TransferFunds(Guid userId, FundAccountRequest request)
		{
			return fundingService.TransferFunds(userId, request);
		}

		[HttpGet("get-ach-relationship/{userId}")]
		public IActionResult GetAchRelationship(Guid userId) 
		{
			return fundingService.GetAchRelationships(userId);
		}

		[HttpPost("cancel-transfer/{userId}/{transferId}")]
		public IActionResult CancelTransfer(Guid userId, Guid transferId) 
		{
			return fundingService.CancelTransfer(userId, transferId);
		}

		[HttpGet("get-all/{userId}")]
		public IActionResult GetTransfers(Guid userId) 
		{
			return fundingService.GetTransfers(userId);
		}
	}
}
