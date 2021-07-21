using ApiClient.Models.Eod;
using Core;
using StockScreener.Database.Model.Price;
using System;
using System.Collections.Generic;

namespace StockAggregation
{
	internal static class EodMappers 
	{
		public static TPriceData MapToPriceData<TPriceData>(string ticker, List<EodCandle> response) where TPriceData : PriceData
		{
			var priceData = Activator.CreateInstance<TPriceData>();
			priceData.Ticker = ticker;

			foreach (var datapoint in response)
			{
				priceData.Candle.Add(new Candle
				{
					timestamp = datapoint.date.ToUnix(),
					openPrice = datapoint.open,
					closePrice = datapoint.close,
					highPrice = datapoint.high,
					lowPrice = datapoint.low
				});
			}

			return priceData;
		}
	}
}
