using Microsoft.AspNetCore.Mvc;
using StockAggregation.Core;

namespace StockAggregation.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
	public class StockFinancialsController : ControllerBase
    {
        private readonly IStockAggregationService stockAggregationService;

        public StockFinancialsController(IStockAggregationService stockAggregationService)
        {
            this.stockAggregationService = stockAggregationService;
        }

        [HttpPost]
        public void UpdateStockFinancials(string market)
        {
            stockAggregationService.UpdateStockFinancialsForMarket(market);
        }
    }
}
