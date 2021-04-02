
using AggregationService.Core;
using ApiClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;

namespace ApiClient
{
	public class PolygonClient : IPolygonClient
	{
		protected string apiKey;
		protected string backupApiKey;

		protected string route = "https://api.polygon.io/";
		protected HttpClient client;

		private Dictionary<TimeResolution, string> TimeResolutionMapper;

		public PolygonClient(ApiSettings settings)
		{
			apiKey = settings.Key;
			backupApiKey = settings.Backup;
			client = new HttpClient();

			TimeResolutionMapper = new Dictionary<TimeResolution, string>
			{
				{ TimeResolution.Minute, "minute" },
				{ TimeResolution.Hour, "hour" },
				{ TimeResolution.Day, "day" },
				{ TimeResolution.Week, "week" },
				{ TimeResolution.Month, "month" },
				{ TimeResolution.Quarter, "quarter" },
				{ TimeResolution.Year, "year" },
			};
		}

		private string GetApiKeyRequestPhrase()
		{
			return "&apiKey=" + apiKey;
		}

		private string GetBackupApiKeyRequestPhrase()
		{
			return "&apiKey=" + backupApiKey;
		}


		public PolygonCompanyInfoResponse GetCompanyInfo(string ticker)
		{
            var request = $"{route}v2/reference/financials/{ticker}?type=Q&sort=reportPeriod";

			return MakeRequest<PolygonCompanyInfoResponse>(request);
		}

		public PolygonPriceDataResponse GetPriceData(string ticker, int interval, TimeResolution timeResolution)
		{
			var request = $"{route}v2/aggs/ticker/{ticker}/range/{interval}/{TimeResolutionMapper[timeResolution]}/2019-03-19/2021-03-19?unadjusted=false&sort=asc&limit=50000";

			return MakeRequest<PolygonPriceDataResponse>(request);
		}

		public PolygonStockFinancialsResponse GetStockFinancials(string ticker)
		{
			var request = $"{route}v2/reference/financials/{ticker}?type=Q&sort=reportPeriod";
			var response = client.GetAsync(request + GetApiKeyRequestPhrase()).Result;

			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				return JsonConvert.DeserializeObject<PolygonStockFinancialsResponse>(response.Content.ReadAsStringAsync().Result);
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
			{
				response = client.GetAsync(request + GetBackupApiKeyRequestPhrase()).Result;

				if (response.StatusCode == System.Net.HttpStatusCode.OK)
				{
					return JsonConvert.DeserializeObject<PolygonStockFinancialsResponse>(response.Content.ReadAsStringAsync().Result);
				}

				else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
				{
					Console.WriteLine("Sleeping For 60 Seconds...");
					Thread.Sleep(60000);
					response = client.GetAsync(request + GetApiKeyRequestPhrase()).Result;

					if (response.StatusCode != System.Net.HttpStatusCode.OK)
					{
						throw new Exception($"Failed to get company info for {ticker}");
					}
				}
			}

			return JsonConvert.DeserializeObject<PolygonStockFinancialsResponse>(response.Content.ReadAsStringAsync().Result);
		}
		private TResponseType MakeRequest<TResponseType>(string request)
		{

			var response = client.GetAsync(request + GetApiKeyRequestPhrase()).Result;

			if ( response.StatusCode == System.Net.HttpStatusCode.OK )
			{
				return DeserializeResponse<TResponseType>(response);
			}
			else if ( response.StatusCode == System.Net.HttpStatusCode.TooManyRequests )
			{
				response = client.GetAsync(request + GetBackupApiKeyRequestPhrase()).Result;

				if ( response.StatusCode == System.Net.HttpStatusCode.OK )
				{
					return DeserializeResponse<TResponseType>(response);
				}

				else if ( response.StatusCode == System.Net.HttpStatusCode.TooManyRequests )
				{
					Console.WriteLine("Sleeping For 60 Seconds...");
					Thread.Sleep(60000);
					response = client.GetAsync(request + GetApiKeyRequestPhrase()).Result;

					if ( response.StatusCode != System.Net.HttpStatusCode.OK )
					{
						throw new Exception($"Failed to ");
					}
				}
			}

			return DeserializeResponse<TResponseType>(response);
		}

		private TResponseType DeserializeResponse<TResponseType>(HttpResponseMessage response)
		{
			return JsonConvert.DeserializeObject<TResponseType>(response.Content.ReadAsStringAsync().Result);
		}
	}
}
