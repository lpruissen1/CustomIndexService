using Microsoft.AspNetCore.Mvc;
using System;
using Users.Core;

namespace Users.Service.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
		private readonly IOrderService orderservice;

		public OrdersController(IOrderService orderservice)
        {
			this.orderservice = orderservice;
		}

		[HttpGet("get-orders/{userId}")]
		public IActionResult GetOrders(Guid userId)
		{
			var order = orderservice.GetOrders(userId);

			if (order is not null)
				return new OkObjectResult(order);

			return new NotFoundResult();
		}

		[HttpDelete("cancel-transfer/{userId}/{orderId}")]
		public IActionResult CancelOrder(Guid userId, Guid orderId)
		{
			var order = orderservice.CancelOrder(userId, orderId);

			if (order)
				return new OkResult();

			return new BadRequestResult();
		}
	}
}
