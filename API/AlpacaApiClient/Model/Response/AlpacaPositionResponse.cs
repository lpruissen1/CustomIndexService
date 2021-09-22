using System;

namespace AlpacaApiClient.Model.Response
{
	public class AlpacaPositionResponse
	{
		public Guid asset_id { get; set; }
		public string symbol { get; set; }
		public string exchange { get; set; }
		public string asset_class { get; set; }
		public decimal avg_entry_price { get; set; }
		public decimal qty { get; set; }
		public string side { get; set; }
		public decimal market_value { get; set; }
		public decimal cost_basis { get; set; }
		public decimal qunrealized_plty { get; set; }
		public decimal unrealized_plpc { get; set; }
		public decimal unrealized_intraday_pl { get; set; }
		public decimal unrealized_intraday_plpc { get; set; }
		public decimal current_price { get; set; }
		public decimal lastday_price { get; set; }
		public decimal change_today { get; set; }
	}
}
