using Microsoft.AspNetCore.Mvc;
using StockScreener.Core.Request;
using StockScreener.Core.Response;

namespace StockScreener.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeightingController : ControllerBase
	{
		private readonly IStockScreenerService screenerService;

		public WeightingController(IStockScreenerService screenerService)
		{
			this.screenerService = screenerService;
		}

		[HttpPost]
		[Consumes("application/json")]
		public WeightingResponse Post(WeightingRequest weightingRequest)
		{
			return screenerService.Weighting(weightingRequest);
		}
	}

	[ApiController]
	[Route("[controller]")]
	public class PurchaseOrderController : ControllerBase
	{
		private readonly IStockScreenerService screenerService;

		public PurchaseOrderController(IStockScreenerService screenerService)
		{
			this.screenerService = screenerService;
		}

		[HttpPost]
		[Consumes("application/json")]
		public PurchaseOrderResponse Post(PurchaseOrderRequest purchaseOrderRequest)
		{
			return screenerService.GetPurchaseOrder(purchaseOrderRequest);
		}
	}
}
