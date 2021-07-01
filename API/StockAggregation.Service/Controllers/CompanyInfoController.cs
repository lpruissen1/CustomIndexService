using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockAggregation.Core;

namespace StockAggregation.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class CompanyInfoController : ControllerBase
    {
        private readonly IStockAggregationService stockAggregationService;
		private readonly ILogger logger;

		public CompanyInfoController(IStockAggregationService stockAggregationService, ILogger logger)
        {
            this.stockAggregationService = stockAggregationService;
			this.logger = logger;
		}

        [HttpPost]
        public void UpdateCompanyInfo(string market)
        {
			logger.LogInformation(new EventId(1), "Updating Company Info");
            stockAggregationService.UpdateCompanyInfoForMarket(market);
        }
    }
}
