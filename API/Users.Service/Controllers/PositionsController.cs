using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class PositionsController : ControllerBase
    {
		private readonly IPositionsService positionsService;

		public PositionsController(IPositionsService positionsService)
        {
			this.positionsService = positionsService;
		}

        [HttpPost("get-all/{userId}")]
        public IActionResult GetAllPositions(Guid userId)
        {
			return positionsService.GetAllPositions(userId);
        }

        [HttpPost("by-portfolio/{userId}/{portfolioId}")]
        public IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId)
        {
			return positionsService.GetPositionsForPortfolio(userId, portfolioId);
        }
	}
}
