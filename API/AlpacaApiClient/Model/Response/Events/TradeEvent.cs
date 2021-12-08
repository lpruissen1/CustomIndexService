using System;

namespace AlpacaApiClient.Model.Response.Events
{
	public class TradeEvent
	{
		public Guid account_id { get; set; }
		public DateTime at { get; set; }
		public TradeEventValue Event { get; set; }
		public int event_id { get; set; }
		public int exercution_id { get; set; }
		public AlpacaOrderResponse order { get; set; }
	}
}
