using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

			var result = await dataService.InitializeClient(new string[] { "blah", "Shit"});

			if (!result)
				return;

			while (dataService.Connected())
			{
				var data = await dataService.Listen();
				await response.WriteAsync(data.Select(x => $"{x.S} + {x.o}").ToString());
				response.Body.Flush();
			}
		}
	}
}
