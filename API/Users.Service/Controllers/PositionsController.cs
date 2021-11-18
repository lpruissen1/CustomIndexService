using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
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

        [HttpGet("get-all-new/{userId}")]
        public async Task<IActionResult> GetAllPositionsNew(Guid userId)
        {
			var result = await positionsService.GetPortfoliosByPortfolio(userId);

			return new OkObjectResult(result);
        }

        [HttpPost("by-portfolio/{userId}/{portfolioId}")]
        public IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId)
        {
			return positionsService.GetPositionsForPortfolio(userId, portfolioId);
        }
	}
}
