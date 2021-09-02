namespace AlpacaApiClient.Model.Request
{
	public class AlpacaAccountDocument
	{
		public DocumentTypeValue document_type { get; set; }
		public string document_sub_type { get; set; }
		public string content { get; set; }
		public string mime_type { get; set; }
	}
}
