
using Core;
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

		private Dictionary<TimePeriod, string> TimeResolutionMapper;

		public PolygonClient(ApiSettings settings)
		{
			apiKey = settings.Key;
			backupApiKey = settings.Backup;
			client = new HttpClient();

			TimeResolutionMapper = new Dictionary<TimePeriod, string>
			{
				{ TimePeriod.Minute, "minute" },
				{ TimePeriod.Hour, "hour" },
				{ TimePeriod.Day, "day" },
				{ TimePeriod.Week, "week" },
				{ TimePeriod.Month, "month" },
				{ TimePeriod.Quarter, "quarter" },
				{ TimePeriod.Year, "year" },
			};
		}

		private string GetApiKeyRequestPhrase()
		{
			return "apiKey=" + apiKey;
		}

		private string GetBackupApiKeyRequestPhrase()
		{
			return "apiKey=" + backupApiKey;
		}


		public PolygonCompanyInfoResponse GetCompanyInfo(string ticker)
		{
            var request = $"{route}v1/meta/symbols/{ticker}/company?";

			return MakeRequest<PolygonCompanyInfoResponse>(request);
		}

		public PolygonPriceDataResponse GetPriceData(string ticker, int interval, TimePeriod timeResolution)
		{
			var request = $"{route}v2/aggs/ticker/{ticker}/range/{interval}/{TimeResolutionMapper[timeResolution]}/2019-03-19/2021-03-19?unadjusted=false&sort=asc&limit=50000&";

			return MakeRequest<PolygonPriceDataResponse>(request);
		}

		public PolygonPriceDataResponse GetPriceData(string ticker, int interval, TimePeriod timeResolution, double start, double end)
		{
			// we multiply by 1000 because polygon uses miliseconds
			var request = $"{route}v2/aggs/ticker/{ticker}/range/{interval}/{TimeResolutionMapper[timeResolution]}/{start * 1000}/{end * 1000}?unadjusted=false&sort=asc&limit=50000&";

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
