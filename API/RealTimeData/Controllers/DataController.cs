using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeData.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
		private IDataService dataService { get; }

		public DataController(IDataService dataService)
		{
			this.dataService = dataService;
		}

		[HttpGet]
		public async Task Get()
		{
			var response = Response;
			response.Headers.Add("Content-Type", "text/event-stream");
			response.Headers.Add("connection", "keep-alive");
			response.Headers.Add("cach-control", "no-cache");

			var result = await dataService.InitializeClient();

			if (!result)
				return;

			await dataService.Subscribe(new string[] { "TSLA", "AAPL" });
			while (dataService.Connected())
			{
				try
				{
					var data = await dataService.Listen();
					await response.WriteAsync(data.Select(x => $"{x.S} + {x.o}").ToString());
					await response.Body.FlushAsync();
				}
				catch(Exception e)
				{
					Console.WriteLine(e);
				}
			}

			var done = 1;
		}
	}
}
