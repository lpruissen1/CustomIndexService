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
}
