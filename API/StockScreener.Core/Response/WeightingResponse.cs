using System.Collections.Generic;

namespace StockScreener.Core.Response
{
	public class WeightingResponse
	{
		public List<WeightingEntry> Tickers { get; set; } 
	}

	public class PurchaseOrderResponse
	{
		public List<PurchaseOrderEntry> Tickers { get; set; } 
	}

	public class PurchaseOrderEntry
	{
		public string Ticker { get; set; }
		public decimal Amount { get; set; }
	}
}
