using Core;
using System;

namespace AlpacaApiClient.Model.Response
{
	public class AlpacaAchRelationshipResponse
	{
		public Guid id { get; set; }
		public string account_number { get; set; }
		public DateTime created_at { get; set; }
		public DateTime updated_at { get; set; }
		public AchStatusValue status { get; set; }
		public string account_owner_name { get; set; }
		public AccountTypeValue bank_account_type { get; set; }
		public string bank_account_number { get; set; }
		public string bank_routing_number { get; set; }
		public string nickname { get; set; }
	}

	public class AlpacaOrderResponse
	{
		public Guid id { get; set; }
		public Guid client_order_id { get; set; }
		public DateTime created_at { get; set; }
		public DateTime? updated_at { get; set; }
		public DateTime? submitted_at { get; set; }
		public DateTime? filled_at { get; set; }
		public DateTime? expired_at { get; set; }
		public DateTime? cancelled_at { get; set; }
		public DateTime? failed_at { get; set; }
		public DateTime? replaced_at { get; set; }
		public Guid? replaced_by { get; set; }
		public Guid? replaces { get; set; }
		public Guid asset_id { get; set; }
		public string symbol { get; set; }
		public string asset_class { get; set; }
		public decimal? notional { get; set; }
		public decimal? qty { get; set; }
		public decimal filled_qty { get; set; }
		public decimal? filled_avg_price { get; set; }
		public string order_class { get; set; }
		public OrderType type { get; set; }
		public string side { get; set; }
		public OrderExecutionTimeframeValue time_in_force { get; set; }
		public decimal? limit_price { get; set; }
		public decimal? stop_price { get; set; }
		public OrderStatusValue status { get; set; }
		public bool extended_hours { get; set; }
		public AlpacaOrderResponse[]? legs { get; set; }
		public decimal? trail_percent { get; set; }
		public decimal? trail_price { get; set; }
		public decimal? hwm { get; set; }
		public decimal commission { get; set; }
	}
}
