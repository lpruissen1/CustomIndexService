using AlpacaApiClient.Model.Response;
using AlpacaApiClient.Model.Response.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServerSentEventsClient.RabbitProducer;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEventsClient
{
	public class Worker : BackgroundService
	{
		private readonly HttpClient client;
		private string route = "https://broker-api.sandbox.alpaca.markets";

		private ILogger logger { get; }
		private IRabbitManager rabbitManager { get; }

		public Worker(ILogger logger, IRabbitManager rabbitManager)
		{
			this.logger = logger;
			this.rabbitManager = rabbitManager;
			client = new HttpClient();
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			InitializeClient(); 
			var orderUrl = $"{route}/v1/events/trades";
			Console.WriteLine("Establishing connection");

			while (!stoppingToken.IsCancellationRequested)
			{
				Console.WriteLine("Establishing connection");
				using (var streamReader = new StreamReader(await client.GetStreamAsync(orderUrl)))
				{
					while (!streamReader.EndOfStream)
					{
						var message = await streamReader.ReadLineAsync();

						Console.WriteLine($"Message: {message}");
						message = $"{{{message}}}";
						Console.WriteLine($"Cleaned message: {message}");
						var result = new Event<TradeEvent>()
						{
							data = new TradeEvent
							{
								Event = TradeEventValue.fill,
								account_id = new Guid("37438406-f906-4f6c-82d5-fcaa38332729"),
								order = new AlpacaOrderResponse
								{
									id = new Guid("c628dbe1-741e-4200-9871-a786d968caed"),
									symbol = "LLL",
									status = Core.OrderStatusValue.filled,
									created_at = DateTime.Now.AddHours(-1),
									filled_at = DateTime.Now,
									type = Core.OrderType.market,
									side = Core.OrderDirectionValue.buy,
									time_in_force = Core.OrderExecutionTimeframeValue.day,
									qty = 69,
									filled_qty = 69,
									filled_avg_price = 100
								}
							}
						};

						if (result is not null && result.data is not null)
						{
							if (result.data.Event == TradeEventValue.fill)
							{
								rabbitManager.Publish(result.data);
							}
						}
					}
				}
			}
			Console.WriteLine("Connection Closed");
		}

		private TResponseType DeserializeResponse<TResponseType>(string response)
		{
			try
			{
				return JsonConvert.DeserializeObject<TResponseType>(response);
			}
			catch
			{
				return default;
			}
		}

		private void InitializeClient()
		{
			client.Timeout = TimeSpan.FromSeconds(2);
			client.DefaultRequestHeaders.Add("Authorization", "Basic " + GetAuthHeader());
		}

		private string GetAuthHeader()
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes("CKXM3IU2N9VWGMI470HF" + ":" + "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H"));
		}
	}
}
