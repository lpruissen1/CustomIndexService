using Microsoft.AspNetCore.Mvc;
using StockScreener.Core.Request;
using StockScreener.Core.Response;

namespace StockScreener.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeightingController : ControllerBase
	{
		private readonly IStockScreenerService screenerService;

		public WeightingController(IStockScreenerService screenerService)
		{
			this.screenerService = screenerService;
		}

		[HttpPost]
		[Consumes("application/json")]
		public WeightingResponse Post(WeightingRequest weightingRequest)
		{
			return screenerService.Weighting(weightingRequest);
		}
	}
}
