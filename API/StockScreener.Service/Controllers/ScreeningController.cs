using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using UserCustomIndices.Model.Response;

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
        public IEnumerable<string> GetByCustomIndexResponse(CustomIndexResponse customIndex)
        {
            return screenerService.Screen(customIndex).GetTickers();
        }
    }
}
