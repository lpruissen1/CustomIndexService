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

		public List<EodExchangeEntry> GetExhangeInfo(string exchange)
		{
			var request = $"{route}exchange-symbol-list/{exchange}?{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<List<EodExchangeEntry>>(request);

			return response;
		}

		public List<EodCandle> GetPriceData(string ticker)
		{
			var request = $"{route}eod/{ticker}?from=2006-01-01&to=2021-07-19&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<List<EodCandle>>(request);

			return response;
		}

		public EodCompanyInfo GetCompanyInfo(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=General&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodCompanyInfo>(request);

			return response;
		}

		public EodEarnings GetEarnings(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=Earnings&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodEarnings>(request);

			response.Ticker = ticker;
			return response;
		}

		public EodOutstandingShares GetOutstandingShares(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=outstandingShares&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodOutstandingShares>(request);

			response.Ticker = ticker;
			return response;
		}

		public EodBalanceSheet GetBalanceSheet(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=Financials::Balance_Sheet&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodBalanceSheet>(request);

			response.Ticker = ticker;
			return response;
		}

		public EodCashFlow GetCashFlow(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=Financials::Cash_Flow&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodCashFlow>(request);

			response.Ticker = ticker;
			return response;
		}

		public EodIncomeStatement GetIncomeStatement(string ticker)
		{
			var request = $"{route}/fundamentals/{ticker}?filter=Financials::Income_Statement&{jsonFormatting}" + GetApiKeyRequestPhrase();
			var response = MakeRequest<EodIncomeStatement>(request);

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

			return DeserializeResponse<TResponseType>(response);
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
