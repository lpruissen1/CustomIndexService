﻿using Microsoft.AspNetCore.Mvc;
using StockScreener.Core.Request;
using StockScreener.Core.Response;
using StockScreener.Mapper;
using System.Linq;

namespace StockScreener.Service.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ScreeningController : ControllerBase
	{
		private readonly IStockScreenerService screenerService;

		public ScreeningController(IStockScreenerService screenerService)
		{
			this.screenerService = screenerService;
		}


		[HttpPost("FuckYourself")]
		[Consumes("application/json")]
		public ScreeningResponse GetByCustomIndexResponse(ScreeningRequest screeningRequest)
		{
			var mapper = new ScreeningResponseMapper();

			return screenerService.Screen(screeningRequest);
		}
	}
}
