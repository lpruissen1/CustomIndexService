using Newtonsoft.Json;
using RealTimeData.Model;
using System;
using System.Collections.Generic;
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
		Task<Trade[]> Listen();
		Task<bool> Subscribe(IEnumerable<string> tickers);
		void Close();
	}

	public class DataService : IDataService
	{
		private ClientWebSocket socket;
		private CancellationToken CTS;
		private readonly Uri url = new Uri("wss://stream.data.sandbox.alpaca.markets/v2/iex");
		private byte[] buffer = new byte[2048];

		public DataService()
		{
			if(socket is not null) 
				socket = new ClientWebSocket();
		}

		public async Task<bool> InitializeClient()
		{

			if (socket is null)
			{
				socket = new ClientWebSocket();
				CTS = new CancellationToken();
			}
			else if (Connected())
				return true;

			var connectionResult = await Connect().ConfigureAwait(false);
			if (!connectionResult)
				return false;

			var authResult = await Authenticate().ConfigureAwait(false);
			if (!authResult)
				return false;

			return true;
		}

		public async Task<bool> Subscribe(IEnumerable<string> tickers)
		{
			var stringedSticker = string.Join(',', tickers.Select(x => $"\"{x}\""));
			var subscription = $"{{ \"action\":\"subscribe\",\"trades\":[{stringedSticker}]}}";
			var uint8array = Encode(subscription);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS);
			var newResult = await socket.ReceiveAsync(buffer, CTS);

			var jsonResponse = DeserializeResponse<Communcation[]>(buffer);
			buffer = new byte[2048];
			return ConfirmResponse(jsonResponse[0]);
		}

		public bool Connected()
		{
			return socket.State == WebSocketState.Open;
		}

		public async Task<Trade[]> Listen()
		{
			try
			{
				var result = await socket.ReceiveAsync(buffer, CTS);
			}
			catch(Exception e)
			{
				Console.WriteLine(e);
			}

			var response = DeserializeResponse<Trade[]>(buffer, false);
			buffer = new byte[2048];
			return response;
		}

		private async Task<bool> Connect()
		{
			await socket.ConnectAsync(url, CTS);
			await socket.ReceiveAsync(buffer, CTS);

			var jsonResponse = DeserializeResponse<Communcation[]>(buffer);
			buffer = new byte[2048];
			return ConfirmResponse(jsonResponse[0]);
		}

		private async Task<bool> Authenticate()	{
			var authString = "{\"action\": \"auth\", \"key\": \"CKXM3IU2N9VWGMI470HF\", \"secret\": \"ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H\"}";
			var uint8array = Encode(authString);

			await socket.SendAsync(uint8array, WebSocketMessageType.Text, true, CTS);
			await socket.ReceiveAsync(buffer, CTS);

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

		public void Close()
		{
			socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CTS);
		}
	}
}
