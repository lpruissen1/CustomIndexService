using System;

namespace AlpacaApiClient.Model.Response
{
	public class AccountStatusResponse
	{
		public string account_id { get; set; }
		public string account_number { get; set; }
		public DateTime at { get; set; }
		public int event_id { get; set; }
		public KycResults kyc_results { get; set; }
		public string status_from { get; set; }
		public string status_to { get; set; }
		public string reason { get; set; }
	}
}
