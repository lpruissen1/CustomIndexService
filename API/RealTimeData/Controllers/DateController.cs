using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RealTimeData.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class DateController : ControllerBase
    {
        public DateController(ILogger<DateController> logger) { }

		[HttpGet]
		public async Task Get()
		{
			var response = Response;
			response.Headers.Add("Content-Type", "text/event-stream");
			response.Headers.Add("connection", "keep-alive");
			response.Headers.Add("cach-control", "no-cache");

			var x = 1;

			while(true)
			{
				await response.WriteAsync($"data: Controller {x} at {DateTime.Now}\r\r");
				x++;
				response.Body.Flush();
				await Task.Delay(5 * 1000);
			}
		}
	}
}
