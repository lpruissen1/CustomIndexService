using Microsoft.AspNetCore.Mvc;
using StockScreener.Core.Request;
using StockScreener.Core.Response;

namespace StockScreener.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ScreeningController : ControllerBase
	{
		private readonly IStockScreenerService screenerService;

		public ScreeningController(IStockScreenerService screenerService)
		{
			this.screenerService = screenerService;
		}


		[HttpPost("FuckYourself")]
		[Consumes("application/json")]
		public ScreeningResponse GetByCustomIndexResponse(ScreeningRequest screeningRequest)
		{
			return screenerService.Screen(screeningRequest);
		}
	}
}
