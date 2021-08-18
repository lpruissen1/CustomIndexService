namespace AlpacaApiClient.Model.Request
{
	public class AlpacaCreateAccountRequest
	{
		public AlpacaAccountContact contact { get; set; }
		public AlpacaAccountIdentity identity { get; set; }
		public AlpacaAccountDisclosures disclosures { get; set; }
		public AlpacaAccountAgreement[] agreements { get; set; }
		public AlpacaAccountDocument[] documents { get; set; }
	}
}
