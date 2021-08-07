using ApiClient;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace AlpacaApiClient
{
	public class AlpacaClient
	{
		protected string apiKey;
		protected string backupApiKey;

		protected string route = "https://broker-api.sandbox.alpaca.markets";
		protected HttpClient client;

		public AlpacaClient(ApiSettings settings)
		{
			apiKey = settings.Key;
			backupApiKey = settings.Backup;
			client = new HttpClient();
		}

		private string GetAuthHeader()
		{
			return "apiKey=" + apiKey;
		}

		private TResponseType MakeRequest<TResponseType>(string request)
		{

			var response = client.GetAsync(request).Result;

			return DeserializeResponse<TResponseType>(response);
		}

		private TResponseType DeserializeResponse<TResponseType>(HttpResponseMessage response)
		{
			return JsonConvert.DeserializeObject<TResponseType>(response.Content.ReadAsStringAsync().Result);
		}
	}
	public struct AlpacaApiSettings
	{
		public string Key { get; set; }
		public string Secret { get; set; }
	}
}
