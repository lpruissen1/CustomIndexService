using Microsoft.AspNetCore.Mvc;
using StockAggregation.Core;

namespace StockAggregation.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class CompanyInfoController : ControllerBase
    {
        private readonly IStockAggregationService stockAggregationService;

        public CompanyInfoController(IStockAggregationService stockAggregationService)
        {
            this.stockAggregationService = stockAggregationService;
        }

        [HttpPost]
        public void UpdateCompanyInfo(string market)
        {
            stockAggregationService.UpdateCompanyInfoForMarket(market);
        }
    }
}
