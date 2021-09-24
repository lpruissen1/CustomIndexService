using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSentEventsClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
		private readonly HttpClient client;
		private string route = "https://broker-api.sandbox.alpaca.markets";

		public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
			client = new HttpClient();
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			InitializeClient();

			var source = "iex";
			var orderUrl = $"{route}/v1/events/trades";
			var url = new Uri($"wss://stream.data.sandbox.alpaca.markets/v2/{source}");

			var WS = new ClientWebSocket();
			var CTS = new CancellationTokenSource();
			await WS.ConnectAsync(url, CTS.Token);

			var buffer = new byte[1024];

			var result = await WS.ReceiveAsync(buffer, CTS.Token);

			if (result is not null)
			{
				var r = Encoding.UTF8.GetString(buffer);
				Console.WriteLine(r.Substring(0, 1024));

			}
			else
			{
				Console.WriteLine("Failure");
				return;
			}
			var sting = "{\"action\": \"auth\", \"key\": \"CKXM3IU2N9VWGMI470HF\", \"secret\": \"ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H\"}";
			var subscription = "{ \"action\":\"subscribe\",\"trades\":[\"AAPL\"],\"quotes\":[\"AMD\",\"CLDR\"],\"bars\":[\"AAPL\",\"VOO\"]}";

			var uint8array = new UTF8Encoding().GetBytes(sting);

			await WS.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);

			var newResult = await WS.ReceiveAsync(buffer, CTS.Token);

			if (newResult is not null)
			{
				var r = Encoding.UTF8.GetString(buffer);
				Console.WriteLine(r.Substring(0, 1024));

			}
			else
			{
				Console.WriteLine("auth Failure");
				return;
			}

			var orderArray = new UTF8Encoding().GetBytes(subscription);

			await WS.SendAsync(orderArray, WebSocketMessageType.Text, true, CTS.Token);

			var subResult = await WS.ReceiveAsync(buffer, CTS.Token);

			if (newResult is not null)
			{
				var r = Encoding.UTF8.GetString(buffer);
				Console.WriteLine(r.Substring(0, 1024));
			}
			else
			{
				Console.WriteLine("sub Failure");
				return;
			}

			while (true)
			{
				Console.WriteLine("Looping");
				var listenResult = await WS.ReceiveAsync(buffer, CTS.Token);

				if (newResult is not null)
				{
					var r = Encoding.UTF8.GetString(buffer);
					Console.WriteLine(r.Substring(0, 1024));
				}
				else
				{
					Console.WriteLine("listening Failure");
					return;
				}

			}

			while (!stoppingToken.IsCancellationRequested)
            {
				Console.WriteLine("Establishing connection");
				using (var streamReader = new StreamReader(await client.GetStreamAsync(orderUrl)))
				{
					while (!streamReader.EndOfStream)
					{
						var message = await streamReader.ReadLineAsync();
						Console.WriteLine($"Data from: {message}");
					}
				}
			}
        }

		private void InitializeClient()
		{
			client.Timeout = TimeSpan.FromSeconds(2);
			client.DefaultRequestHeaders.Add("Authorization", "Basic " + GetAuthHeader());
		}

		private void Subscribe()
		{
			string url = $"{route}/v1/events/trades";

			var request = new HttpRequestMessage(HttpMethod.Get, url);

			client.SendAsync(request);
		}

		private string GetAuthHeader()
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes("CKXM3IU2N9VWGMI470HF" + ":" + "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H"));
		}
	}

	public class AuthRequest
{
	string action { get; set; }
		string key { get; set; }
		string secret { get; set; }}
}
