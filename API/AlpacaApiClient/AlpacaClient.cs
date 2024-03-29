﻿using AlpacaApiClient.Model.Request;
using AlpacaApiClient.Model.Response;
using Core;
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

			// logic here to map to new fields
			if(response.StatusCode == System.Net.HttpStatusCode.Forbidden)
				return new AlpacaTransferRequestResponse() { Code = 403, Message = "Insufficient funds"};

			return new AlpacaTransferRequestResponse() { Code = 69, Message = "Get Fucked" };
		}

		public AlpacaAccountHistoryResponse AccountHistory(Guid accountId, TimePeriod timePeriod)
		{
			var alpacaTimePeriod = GetAlpacaTimePeriod(timePeriod);
			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/trading/accounts/{accountId}/account/portfolio/history?period={alpacaTimePeriod}");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AlpacaAccountHistoryResponse>(response);

			logger.LogInformation(new EventId(1), $"Error Requesting account history: {GetStringFromStream(response.Content.ReadAsStream())}");

			// logic here to map to new fields
			if(response.StatusCode == System.Net.HttpStatusCode.Forbidden)
				return new AlpacaAccountHistoryResponse() { Code = 403, Message = "Insufficient funds"};

			return new AlpacaAccountHistoryResponse() { Code = 69, Message = "Get Fucked" };
		}

		private string GetAlpacaTimePeriod(TimePeriod timePeriod)
		{
			switch(timePeriod) {
				case TimePeriod.FiveYear:
					return "5A";
				case TimePeriod.ThreeYear:
					return "3A";
				case TimePeriod.Year:
					return "1A";
				case TimePeriod.HalfYear:
					return "6M";
				case TimePeriod.Quarter:
					return "3M";
				case TimePeriod.Month:
					return "1M";
				default:
					return "1A";
			}
		}

		public bool CancelTransfer(Guid accountId, Guid transferId)
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, $"{route}/v1/accounts/{accountId}/transfers/{transferId}");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return true;

			logger.LogInformation(new EventId(1), $"Error cancelling transfering funds: {GetStringFromStream(response.Content.ReadAsStream())}");

			return false;
		}

		public bool CancelOrder(Guid accountId, Guid orderId)
		{
			var request = new HttpRequestMessage(HttpMethod.Delete, $"{route}/v1/trading/accounts/{accountId}/orders/{orderId}");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.NoContent)
				return true;

			logger.LogInformation(new EventId(1), $"Error cancelling order: {GetStringFromStream(response.Content.ReadAsStream())}");

			return false;
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

		public AlpacaPositionResponse[] GetAllPositions(Guid accountId)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/trading/accounts/{accountId}/positions");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AlpacaPositionResponse[]>(response);

			logger.LogInformation(new EventId(1), $"Error executing order: {GetStringFromStream(response.Content.ReadAsStream())}");

			return default;
		}

		public AlpacaPositionResponse GetPosition(Guid accountId, string ticker)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, $"{route}/v1/trading/accounts/{accountId}/positions/{ticker}");
			request.Headers.Add("Authorization", "Basic " + GetAuthHeader());

			var response = client.SendAsync(request).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
				return DeserializeResponse<AlpacaPositionResponse>(response);

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
