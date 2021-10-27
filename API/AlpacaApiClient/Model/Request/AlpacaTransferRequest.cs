using Core;
using System;

namespace AlpacaApiClient.Model.Request
{
	public class AlpacaTransferRequest
	{
		public TransferTypeValue transfer_type { get; set; }
		public Guid relationship_id { get; set; }
		public string amount { get; set; }
		public TransferDirectionValue direction { get; set; }
	}

	public class AlpacaMarketOrderRequest
	{
		public string symbol { get; set; }
		public decimal? qty { get; set; } // share quantity
		public decimal? notional { get; set; } // dollar amount
		public OrderDirectionValue side { get; set; }
		public OrderType type { get; set; }
		public OrderExecutionTimeframeValue time_in_force { get; set; }
	}
}
