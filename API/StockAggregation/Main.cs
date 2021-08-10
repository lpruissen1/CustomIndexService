﻿using ApiClient;
using Core.Logging;
using StockScreener.Database;
using StockScreener.Database.Config;

namespace StockAggregation
{
	public class Program
	{
		static public void Main()
		{

			var stockDBSettings = new StockInformationDatabaseSettings() { ConnectionString = "mongodb://localhost:27017", DatabaseName = "StockData" };
			var priceDBSettings = new StockInformationDatabaseSettings() { ConnectionString = "mongodb://localhost:27017", DatabaseName = "PriceData" };


			var client = new EodClient(new ApiSettings() { Key = "60ecf3337feaa7.96969903" }, new MyLogger(new MyLoggerOptions() { FolderPath = "C:\\Log\\EodApiClient\\", FilePath = "log.log" }));
			//var blah = client.Load("GSPC.INDX");
			var sut = new EodStockAggregationService(new MongoStockInformationDbContext(stockDBSettings), new MongoStockInformationDbContext(priceDBSettings), client, new MyLogger(new MyLoggerOptions() { FolderPath = "C:\\Log\\EodAgg\\", FilePath = "log.log" }));
			sut.LoadTickersByIndex("GSPC.INDX");
			sut.LoadStockData("GSPC.INDX");
		}
	}
}
