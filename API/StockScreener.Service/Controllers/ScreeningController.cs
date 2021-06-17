using Microsoft.AspNetCore.Mvc;
using StockScreener.Core.Request;
using System.Collections.Generic;

namespace StockScreener.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScreeningController : ControllerBase
    {
        public ScreeningController(IStockScreenerService screenerService)
        {
            this.screenerService = screenerService;
        }

        private IStockScreenerService screenerService;

        [HttpPost("FuckYourself")]
        [Consumes("application/json")]
        public IEnumerable<string> GetByCustomIndexResponse(ScreeningRequest screeningRequest)
        {
            return screenerService.Screen(screeningRequest).GetTickers();
        }
    }
}
