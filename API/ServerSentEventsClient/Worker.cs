using AlpacaApiClient.Model.Response.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Users;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace ServerSentEventsClient
{
	public class Worker : BackgroundService
	{
		private readonly HttpClient client;
		private string route = "https://broker-api.sandbox.alpaca.markets";

		public ILogger logger { get; }
		public IUserAccountsRepository userAccountsRepository { get; }
		public IUserOrdersRepository userOrdersRepository { get; }
		public IPositionAdditionHandler positionAdditionHandler { get; }

		public Worker(ILogger logger, IUserAccountsRepository userAccountsRepository, IUserOrdersRepository userOrdersRepository, IPositionAdditionHandler positionAdditionHandler)
		{
			this.logger = logger;
			this.userAccountsRepository = userAccountsRepository;
			this.userOrdersRepository = userOrdersRepository;
			this.positionAdditionHandler = positionAdditionHandler;
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
						//var cleanedMessage = CleanResponse(message);
						Console.WriteLine($"Cleaned message: {message}");
						var result = DeserializeResponse<Event<TradeEvent>>(message);

						if (result is not null && result.data is not null)
						{
							if (result.data.Event == TradeEventValue.fill)
							{
								var relatedUser = userAccountsRepository.GetByAccountId(result.data.account_id).UserId;
								var userOrders = userOrdersRepository.GetByUserId(relatedUser).Orders;
								var relatedOrder = userOrders.FirstOrDefault(x => x.OrderId == result.data.order.client_order_id);
								if (relatedOrder is not null)
								{
									userOrdersRepository.FillOrder(relatedUser, relatedOrder.OrderId);
									logger.LogInformation($"Filled order for accout, {result.data.account_id}, order for {result.data.order.symbol}");
									var newPosition = new Position(result.data.order.symbol, result.data.order.filled_avg_price.Value, relatedOrder.PortfolioId, result.data.order.filled_qty);

									positionAdditionHandler.AddPosition(relatedUser, newPosition);
								}
								else
								{
									logger.LogInformation(new EventId(1), "Failed to find order");
								}
							}
						}
					}
				}
			}
			Console.WriteLine("Connection Closed");
		}

		private string CleanResponse(string response)
		{
			return response.Remove(0, 6); 
		}

		private TResponseType DeserializeResponse<TResponseType>(string response)
		{
			try
			{
				return JsonConvert.DeserializeObject<TResponseType>(response);
			}
			catch
			{
			}

			return default;
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
