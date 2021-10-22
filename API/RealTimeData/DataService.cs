using Newtonsoft.Json;
using RealTimeData.Model;
using System;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeData
{
	public interface IDataService
	{
		Task<bool> InitializeClient();
		bool Connected();
		Task<MinuteBar[]> Listen();
		Task<bool> Subscribe(string[] tickers);
	}

	public class DataService : IDataService
	{
		private ClientWebSocket socket;
		private readonly Uri url = new Uri("wss://stream.data.sandbox.alpaca.markets/v2/iex");
		private CancellationTokenSource CTS;
		private byte[] buffer;

		public DataService()
		{
			socket = new ClientWebSocket();
			CTS = new CancellationTokenSource();
			buffer = new byte[2048];
		}

		public async Task<bool> InitializeClient()
		{
			if (Connected())
				return true;

			var connectionResult = await Connect().ConfigureAwait(false);
			if (!connectionResult)
				return false;

			var authResult = await Authenticate().ConfigureAwait(false);
			if (!authResult)
				return false;

			return true;
		}

		public async Task<bool> Subscribe(string[] tickers)
		{
			var stringedSticker = tickers.Select(x => $"\"{x}\" + ,");
			var subscription = $"{{ \"action\":\"subscribe\",\"bars\":[\"AAPL\"]}}";
			var uint8array = Encode(subscription);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);
			var newResult = await socket.ReceiveAsync(buffer, CTS.Token);

			var jsonResponse = DeserializeResponse<Communcation[]>(buffer);
			buffer = new byte[2048];
			return ConfirmResponse(jsonResponse[0]);
		}

		public bool Connected()
		{
			return (socket.State == WebSocketState.Open) && !CTS.IsCancellationRequested;
		}

		public async Task<MinuteBar[]> Listen()
		{
			try
			{
				var result = await socket.ReceiveAsync(buffer, CTS.Token);
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}

			var response = DeserializeResponse<MinuteBar[]>(buffer, false);
			buffer = new byte[2048];
			return response;
		}

		private async Task<bool> Connect()
		{
			await socket.ConnectAsync(url, CTS.Token);
			await socket.ReceiveAsync(buffer, CTS.Token);

			var jsonResponse = DeserializeResponse<Communcation[]>(buffer);
			buffer = new byte[2048];
			return ConfirmResponse(jsonResponse[0]);
		}

		private async Task<bool> Authenticate()	{
			var authString = "{\"action\": \"auth\", \"key\": \"CKXM3IU2N9VWGMI470HF\", \"secret\": \"ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H\"}";
			var uint8array = Encode(authString);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS.Token);
			await socket.ReceiveAsync(buffer, CTS.Token);

			var jsonResponse = DeserializeResponse<Communcation[]>(buffer);

			return ConfirmAuthResponse(jsonResponse[0]);
		}

		private bool ConfirmResponse(Communcation respone)
		{
			if (respone.T == "success")
				return true;

			return false;
		}

		private bool ConfirmAuthResponse(Communcation response)
		{
			if (response.T == "success" || response.msg == "auth timeout")
				return true;

			return false;
		}

		private byte[] Encode(string thingToEncode)
		{
			return new UTF8Encoding().GetBytes(thingToEncode);
		}

		private TResponseType DeserializeResponse<TResponseType>(byte[] data, bool trim = true)
		{
			try
			{
				using (StreamReader sr = new StreamReader(new MemoryStream(data)))
				{
					var stream = sr.ReadToEnd();
					var trimmed = stream.TrimEnd('\0');
					//var good = trimmed.Substring(1, trimmed.Length - 2);
					Console.WriteLine("String to deserialize: " + trimmed);
					return JsonConvert.DeserializeObject<TResponseType>(trimmed);
				}
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
				Console.WriteLine("Get fucked by de-cherrios");
			}

			return default;
		}
	}
}
