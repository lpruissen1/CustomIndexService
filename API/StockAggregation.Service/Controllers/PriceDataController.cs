using Microsoft.AspNetCore.Mvc;
using StockAggregation.Core;

namespace StockAggregation.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class PriceDataController : ControllerBase
    {
        private readonly IStockAggregationService stockAggregationService;

        public PriceDataController(IStockAggregationService stockAggregationService)
        {
            this.stockAggregationService = stockAggregationService;
        }

        [HttpPost("Daily")]
        public void UpdateDailyPriceInfo(string market)
        {
            stockAggregationService.UpdateDailyPriceDataForMarket(market);
        }

        [HttpPost("Hourly")]
        public void UpdateHourlyInfo(string market)
        {
            stockAggregationService.UpdateHourlyPriceDataForMarket(market);
        }
    }
}
