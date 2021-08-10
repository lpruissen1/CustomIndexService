using ApiClient.Models.Eod;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace ApiClient
{
	public class EodClient : IEodClient
	{
		private string route => "https://eodhistoricaldata.com/api/";
		private string jsonFormatting => "fmt=json&";
		private ApiSettings apiSettings { get; }
		private ILogger logger { get; }
		private HttpClient client { get; }

		public EodClient(ApiSettings apiSettings, ILogger logger)
		{
			this.apiSettings = apiSettings;
			this.logger = logger;
			client = new HttpClient();
		}

		private string GetApiKeyRequestPhrase()
		{
			return "api_token=" + apiSettings.Key;
		}

		public List<EodCandle> GetPriceData(string ticker)
		{
			var now = DateTime.Now.ToString();
			var request = $"{route}eod/{ticker}?{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<List<EodCandle>>(request);

			return response;
		}

		public EodIndex GetIndexInfo(string exchange)
		{
			var request = $"{route}fundamentals/{exchange}?{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodIndex>(request);

			return response;
		}

		public EodFundementals GetFundementals(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodFundementals>(request);

			response.Ticker = ticker;
			return response;
		}

		private TResponseType MakeRequest<TResponseType>(string request)
		{
			var response = client.GetAsync(request).Result;

			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				logger.LogInformation(new EventId(1), $"Failed Request: {request} with status code {response.StatusCode} for {response.ReasonPhrase}");
				return default;
			}

			return DeserializeResponse<TResponseType>(response) ?? default;
		}

		private TResponseType DeserializeResponse<TResponseType>(HttpResponseMessage response)
		{
			try
			{
				var blah = response.Content.ReadAsStringAsync().Result;
				return JsonConvert.DeserializeObject<TResponseType>(blah);
			}
			catch (Exception e)
			{
				logger.LogInformation(new EventId(1), $"Failed tp deserialize request: {e.Message}");
			}

			return default;
		}
	}
}
