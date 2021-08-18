using System;

namespace AlpacaApiClient.Model.Response
{
	public class AlpacaCreateAccountResponse
	{
		public Guid id { get; set; }
		public string account_number { get; set; }
		public AlpacaAccountStatusValue status { get; set; }
		public string currency { get; set; }
		public string last_equity { get; set; }
		public DateTime created_at { get; set; }
	}
}
