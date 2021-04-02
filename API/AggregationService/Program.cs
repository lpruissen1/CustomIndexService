using System;
using System.Collections.Generic;

namespace AggregationService
{
	public class Program
	{
		protected static StockAggregationService service = StockAggregationService.New();

		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Architech's Data Aggregation CLI");
			Console.WriteLine();
			Console.WriteLine("Aggregation Services:");
			Console.WriteLine();
			Console.WriteLine("To Aggregate 'CompanyInfo': [a]");
			Console.WriteLine("To Aggregate 'StockFinancials': [b]");
			Console.WriteLine("To Aggregate 'Price Data': [c]");
			Console.WriteLine();
			Console.WriteLine("Reset Services:");
			Console.WriteLine();
			Console.WriteLine("To Reset 'StockFinancials': [d]");
			Console.WriteLine("To Reset 'Price data': [e]");
			Console.WriteLine();
			Console.WriteLine("Update Services:");
			Console.WriteLine();
			Console.WriteLine("To Update 'CompanyInfo': [f]");
			Console.WriteLine("To Update 'StockFinancials': [g]");
			Console.WriteLine("To Update 'Price data': [h]");
			Console.WriteLine();
			Console.WriteLine("To Close The Program: [z]");
			Console.WriteLine();
			Console.Write("> ");


			string action = Console.ReadLine();

			while (action != "z")
			{
				var tickers = GetTickers();

				switch (action)
				{
					case "a":
						LoopThroughTickers(service.InsertCompanyInfo, tickers);
						break;

					case "b":
						LoopThroughTickers(service.InsertStockFinancials, tickers);
						break;

					case "c":
						InsertPriceData(tickers);
						break;

					case "d":
						service.ClearStockFinancials();
						break;

					case "e":
						service.ClearPriceData();
						break;

					case "f":
						LoopThroughTickers(service.UpdateCompanyInfo, tickers);
						break;

					case "g":
						LoopThroughTickers(service.UpdateStockFinancials, tickers);
						break;

					case "h":
						UpdatePriceData(tickers);						
						break;

					default:
						Console.WriteLine("Invalid Input");
						break;
				}

				Console.Write("> ");
				action = Console.ReadLine();
			}
		}

		private static void LoopThroughTickers(Action<string> operation, IEnumerable<string> tickers)
		{
			foreach(var ticker in tickers)
			{
				operation(ticker);
			}
		}

		private static void InsertPriceData(IEnumerable<string> tickers)
		{
			Console.WriteLine("What time interval?");
			Console.WriteLine("Hourly");
			Console.WriteLine("Daily");
			switch ( Console.ReadLine().ToLower() )
			{
				case "hourly":
					LoopThroughTickers(service.InsertHourlyPriceData, tickers);
					break;
				case "daily":
					LoopThroughTickers(service.InsertDailyPriceData, tickers);
					break;
				default:
					Console.WriteLine("Fuck you");
					break;
			}
		}
		

		private static void UpdatePriceData(IEnumerable<string> tickers)
		{
			Console.WriteLine("What time interval?");
			Console.WriteLine("Hourly");
			Console.WriteLine("Daily");
			switch ( Console.ReadLine().ToLower() )
			{
				case "hourly":
					LoopThroughTickers(service.UpdateHourlyPriceData, tickers);
					break;
				case "daily":
					LoopThroughTickers(service.UpdateDailyPriceData, tickers);
					break;
				default:
					Console.WriteLine("Fuck you");
					break;
			}
		}

		public static IEnumerable<string> GetTickers()
		{
			Console.WriteLine("Select an Index");
			foreach (var index in service.GetStockIndicesNames())
			{
				Console.WriteLine();
				Console.WriteLine(index);
				Console.WriteLine();
			}

			Console.Write("> ");
			return service.GetTickersByIndex(Console.ReadLine());
		}
	}
}
