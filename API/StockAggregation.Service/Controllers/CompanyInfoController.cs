using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockAggregation.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
