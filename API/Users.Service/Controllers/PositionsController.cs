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

        [HttpPost("create")]
        public IActionResult GetAllPositions(Guid userId)
        {
			return positionsService.GetAllPositions(userId);
        }

        [HttpPost("create-ach-relationship/{userId}")]
        public IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId)
        {
			return positionsService.GetPositionsForPortfolio(userId, portfolioId);
        }
	}
}
