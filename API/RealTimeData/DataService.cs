using Newtonsoft.Json;
using RealTimeData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeData
{
	public class DataService
	{
		private ClientWebSocket socket;
		private readonly Uri url = new Uri("wss://stream.data.sandbox.alpaca.markets/v2/iex");
		private CancellationTokenSource CTS;
		private byte[] buffer;

		public DataService()
		{
			socket = new ClientWebSocket();
			CTS = new CancellationTokenSource();
			buffer = new byte[1024];
		}

		public async Task<bool> InitializeClient(IEnumerable<string> tickers)
		{
			var connectionResult = await Connect().ConfigureAwait(false);
			if (!connectionResult)
				return false;

			var authResult = await Authenticate().ConfigureAwait(false);
			if (!authResult)
				return false;

			return true;
		}



		public async Task<bool> Connect()
		{
			await socket.ConnectAsync(url, CTS.Token);
			await socket.ReceiveAsync(buffer, CTS.Token);

			var jsonResponse = DeserializeResponse<Communcation>(DecodeThisDick(buffer));

			if (jsonResponse.T == "success")
				return true;

			return false;
		}

		public async Task<bool> Authenticate()
		{
			var authString = "{\"action\": \"auth\", \"key\": \"CKXM3IU2N9VWGMI470HF\", \"secret\": \"ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H\"}";
			var uint8array = Encode(authString);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);
			var newResult = await socket.ReceiveAsync(buffer, CTS.Token);
			var jsonResponse = DeserializeResponse<Communcation>(DecodeThisDick(buffer));

			if (jsonResponse.T == "success")
				return true;

			return false;
		}

		public async Task<bool> Subscribe(IEnumerable<string> tickers)
		{
			var stringedSticker = tickers.Select(x => $"\"{x}\" + ,");
			var subscription = $"{{ \"action\":\"subscribe\",\"trades\":[{stringedSticker}],\"quotes\":[\"AMD\",\"CLDR\"],\"bars\":[\"AAPL\",\"VOO\"]}}";
			var uint8array = Encode(subscription);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);
			var newResult = await socket.ReceiveAsync(buffer, CTS.Token);
			var jsonResponse = DeserializeResponse<Communcation>(DecodeThisDick(buffer));

			if (jsonResponse.T == "success")
				return true;

			return false;
		}

		private byte[] Encode(string thingToEncode)
		{
			return new UTF8Encoding().GetBytes(thingToEncode);
		}

		private string DecodeThisDick(byte[] dickToDecode)
		{
			return Encoding.UTF8.GetString(dickToDecode);
		}

		private TResponseType DeserializeResponse<TResponseType>(string response)
		{
			try
			{
				return JsonConvert.DeserializeObject<TResponseType>(response);
			}
			catch
			{
				Console.WriteLine("Get fucked by de-cherrios");
			}

			return default;
		}

		//protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		//{
		//	InitializeClient();

		//	var source = "iex";
		//	var orderUrl = $"{route}/v1/events/trades";
		//	var url = new Uri($"wss://stream.data.sandbox.alpaca.markets/v2/{source}");

		//	var WS = new ClientWebSocket();
		//	var CTS = new CancellationTokenSource();
		//	await WS.ConnectAsync(url, CTS.Token);

		//	var buffer = new byte[1024];

		//	var result = await WS.ReceiveAsync(buffer, CTS.Token);

		//	if (result is not null)
		//	{
		//		var r = Encoding.UTF8.GetString(buffer);
		//		Console.WriteLine(r.Substring(0, 1024));

		//	}
		//	else
		//	{
		//		Console.WriteLine("Failure");
		//		return;
		//	}
		//	var sting = "{\"action\": \"auth\", \"key\": \"CKXM3IU2N9VWGMI470HF\", \"secret\": \"ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H\"}";
		//	var subscription = "{ \"action\":\"subscribe\",\"trades\":[\"AAPL\"],\"quotes\":[\"AMD\",\"CLDR\"],\"bars\":[\"AAPL\",\"VOO\"]}";

		//	var uint8array = new UTF8Encoding().GetBytes(sting);

		//	await WS.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);

		//	var newResult = await WS.ReceiveAsync(buffer, CTS.Token);

		//	if (newResult is not null)
		//	{
		//		var r = Encoding.UTF8.GetString(buffer);
		//		Console.WriteLine(r.Substring(0, 1024));

		//	}
		//	else
		//	{
		//		Console.WriteLine("auth Failure");
		//		return;
		//	}

		//	var orderArray = new UTF8Encoding().GetBytes(subscription);

		//	await WS.SendAsync(orderArray, WebSocketMessageType.Text, true, CTS.Token);

		//	var subResult = await WS.ReceiveAsync(buffer, CTS.Token);

		//	if (newResult is not null)
		//	{
		//		var r = Encoding.UTF8.GetString(buffer);
		//		Console.WriteLine(r.Substring(0, 1024));
		//	}
		//	else
		//	{
		//		Console.WriteLine("sub Failure");
		//		return;
		//	}

		//	while (true)
		//	{
		//		Console.WriteLine("Looping");
		//		var listenResult = await WS.ReceiveAsync(buffer, CTS.Token);

		//		if (newResult is not null)
		//		{
		//			var r = Encoding.UTF8.GetString(buffer);
		//			Console.WriteLine(r.Substring(0, 1024));
		//		}
		//		else
		//		{
		//			Console.WriteLine("listening Failure");
		//			return;
		//		}

		//	}

		//	while (!stoppingToken.IsCancellationRequested)
		//	{
		//		Console.WriteLine("Establishing connection");
		//		using (var streamReader = new StreamReader(await client.GetStreamAsync(orderUrl)))
		//		{
		//			while (!streamReader.EndOfStream)
		//			{
		//				var message = await streamReader.ReadLineAsync();
		//				Console.WriteLine($"Data from: {message}");
		//			}
		//		}
		//	}
		//}
	}
}
