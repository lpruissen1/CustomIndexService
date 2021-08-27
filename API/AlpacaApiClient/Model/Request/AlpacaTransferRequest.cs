namespace AlpacaApiClient.Model.Request
{
	public class AlpacaTransferRequest
	{
		public string transfer_type { get; set; }
		public string relationship_id { get; set; }
		public string amount { get; set; }
		public string direction { get; set; }
	}
}
