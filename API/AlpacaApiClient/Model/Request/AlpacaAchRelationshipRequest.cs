namespace AlpacaApiClient.Model.Request
{
	public class AlpacaAchRelationshipRequest
	{
		public string account_owner_name { get; set; }
		public string bank_account_type { get; set; }
		public string bank_account_number { get; set; }
		public string bank_routing_number { get; set; }
		public string nickname { get; set; }
	}
}
