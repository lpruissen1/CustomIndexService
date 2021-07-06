using Microsoft.AspNetCore.Mvc;
using StockInformation;
using System.Collections.Generic;

namespace StockScreener.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StockInformationController : ControllerBase
	{
		private readonly IStockInformationService stockInformationService;

		public StockInformationController(IStockInformationService stockInformationService)
		{
			this.stockInformationService = stockInformationService;
		}

		[HttpGet("GetAllTickers")]
        [Consumes("application/json")]
        public IEnumerable<string> GetAllTickers()
        {
            return stockInformationService.GetAllTickers();
        }
    }
}
