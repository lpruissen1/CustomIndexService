using AlpacaApiClient.Model.Request;
using AlpacaApiClient.Model.Response;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
		private ILogger logger { get; }

		public AlpacaClient(AlpacaApiSettings settings, ILogger logger)
		{
			key = settings.Key;
			secret = settings.Secret; ;
			client = new HttpClient();

			client.DefaultRequestHeaders.Add("Authorization", "Basic " + GetAuthHeader());
			this.logger = logger;
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
				return DeserializeResponse<AlpacaCreateAccountResponse>(response);


			logger.LogInformation(new EventId(1), $"Error creating account: {GetStringFromStream(response.Content.ReadAsStream())}");

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

		public AssetResponse[] GetAssets() 
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/assets");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AssetResponse[]>(response);


			logger.LogInformation(new EventId(1), $"Error getting assets: {GetStringFromStream(response.Content.ReadAsStream())}");

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
				return DeserializeResponse<AlpacaAchRelationshipResponse>(response);


			logger.LogInformation(new EventId(1), $"Error creating ach relationship: {GetStringFromStream(response.Content.ReadAsStream())}");

			return default;
		}

		public List<AlpacaAchRelationshipResponse> GetAchRelationships(Guid accountId) {

			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/accounts/{accountId}/ach_relationships");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<List<AlpacaAchRelationshipResponse>>(response);


			logger.LogInformation(new EventId(1), $"Error getting ach relationship: {GetStringFromStream(response.Content.ReadAsStream())}");

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
				return DeserializeResponse<AlpacaTransferRequestResponse>(response);


			logger.LogInformation(new EventId(1), $"Error transfering funds: {GetStringFromStream(response.Content.ReadAsStream())}");

			return default;
		}

		public AlpacaTransferRequestResponse CancelTransfer(Guid accountId, Guid transferId)
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, $"{route}/v1/accounts/{accountId}/transfers/{transferId}");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AlpacaTransferRequestResponse>(response);

			logger.LogInformation(new EventId(1), $"Error transfering funds: {GetStringFromStream(response.Content.ReadAsStream())}");

			return default;
		}

		public AlpacaOrderResponse ExecuteOrder(AlpacaMarketOrderRequest alpacaRequest, Guid accountId)
		{
			var options = new JsonSerializerOptions();
			options.Converters.Add(new JsonStringEnumConverter());

			string json = System.Text.Json.JsonSerializer.Serialize(alpacaRequest, options);
			var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

			var request = new HttpRequestMessage(HttpMethod.Post, $"{route}/v1/trading/accounts/{accountId}/orders");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());
			request.Content = httpContent;

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AlpacaOrderResponse>(response);

			logger.LogInformation(new EventId(1), $"Error executing order: {GetStringFromStream(response.Content.ReadAsStream())}");

			return default;
		}

		private string GetAuthHeader()
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(key + ":" + secret));
		}

		private TResponseType DeserializeResponse<TResponseType>(HttpResponseMessage response)
		{
			try
			{
				return JsonConvert.DeserializeObject<TResponseType>(response.Content.ReadAsStringAsync().Result);
			}
			catch
			{
				logger.LogInformation(new EventId(1), $"Error deserializing object: {typeof(TResponseType)}");
			}
			
			return default;
		}

		private string GetStringFromStream(Stream stream)
		{
			var buffer = new byte[stream.Length];

			stream.Read(buffer, 0, buffer.Length);

			return Encoding.UTF8.GetString(buffer);
		}
	}
}
