namespace AlpacaApiClient.Model.Response
{
	public class KycResults
	{
		public AlpacaKycResults[] reject { get; set; }
		public AlpacaKycResults[] accept { get; set; }
		public AlpacaKycResults[] indeterminate { get; set; }

	}
}
