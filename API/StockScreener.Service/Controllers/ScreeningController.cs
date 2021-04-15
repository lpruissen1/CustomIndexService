using Database.Model.User.CustomIndices;
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

        [HttpPost]
        public IEnumerable<string> Get(CustomIndex customIndex)
        {
            return screenerService.Screen(customIndex).GetTickers();
        }

        [HttpPost("FuckYourself")]
        public IEnumerable<string> GetByCustomIndexResponse(CustomIndexResponse customIndex)
        {
            return screenerService.Screen(customIndex).GetTickers();
        }
    }
}
