using AlpacaApiClient.Model.Request;
using AlpacaApiClient.Model.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AlpacaApiClient
{
	public class AlpacaClient
	{
		private string key;
		private string secret;

		private string route = "https://broker-api.sandbox.alpaca.markets";
		private HttpClient client;

		public AlpacaClient(AlpacaApiSettings settings)
		{
			key = settings.Key;
			secret = settings.Secret; ;
			client = new HttpClient();

			client.DefaultRequestHeaders.Add("Authorization", "Basic " + GetAuthHeader());
		}

		public AlpacaCreateAccountResponse CreateAccount(AlpacaCreateAccountRequest alpacaRequest) 
		{
			var options = new JsonSerializerOptions();
			options.Converters.Add(new JsonStringEnumConverter());
			string json = System.Text.Json.JsonSerializer.Serialize(alpacaRequest, options);

			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var request = new HttpRequestMessage(HttpMethod.Post, $"{route}/v1/accounts");
			request.Content = httpContent;

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<AlpacaCreateAccountResponse>(blah);
			}
			
			// log rejection reason
			var buffer = new byte[100000];
			response.Content.ReadAsStream().Read(buffer, 0, buffer.Length);
			var th = Encoding.UTF8.GetString(buffer);
			return default;
		}

		public AccountStatusResponse[] GetAccountStatus()
		{
			var options = new JsonSerializerOptions();
			options.Converters.Add(new JsonStringEnumConverter());

			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/events/accounts/status");

			var response = client.GetStreamAsync($"{route}/v1/events/accounts/status").Result;

			if (response.CanRead)
			{
				var buffer = new byte[100000];
				response.Read(buffer, 0, buffer.Length);
				var th = Encoding.UTF8.GetString(buffer);
				return default;
			}

			return default;
		}

		public AssetResponse[] GetAsset() 
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/assets");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<AssetResponse[]>(blah);
			}

			return default;
		}

		public AlpacaAchRelationshipResponse CreateAchRelationsip(AlpacaAchRelationshipRequest alpacaRequest, Guid accountId) 
		{
			string json = System.Text.Json.JsonSerializer.Serialize(alpacaRequest);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var request = new HttpRequestMessage(HttpMethod.Post, $"{route}/v1/accounts/{accountId}/ach_relationships");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());
			request.Content = httpContent;

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<AlpacaAchRelationshipResponse>(blah);
			}

			return default;
		}

		public List<AlpacaAchRelationshipResponse> GetAchRelationships(Guid accountId) {

			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/accounts/{accountId}/ach_relationships");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<List<AlpacaAchRelationshipResponse>>(blah);
			}

			return default;
		}

		public AlpacaTransferRequestResponse TransferFunds(AlpacaTransferRequest alpacaRequest, Guid accountId)
		{
			var options = new JsonSerializerOptions();
			options.Converters.Add(new JsonStringEnumConverter());

			string json = System.Text.Json.JsonSerializer.Serialize(alpacaRequest, options);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var request = new HttpRequestMessage(HttpMethod.Post, $"{route}/v1/accounts/{accountId}/transfers");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());
			request.Content = httpContent;

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<AlpacaTransferRequestResponse>(blah);
			}

			var buffer = new byte[100000];
			response.Content.ReadAsStream().Read(buffer, 0, buffer.Length);
			var th = Encoding.UTF8.GetString(buffer);
			return default;
		}

		private string GetAuthHeader()
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(key + ":" + secret));
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
