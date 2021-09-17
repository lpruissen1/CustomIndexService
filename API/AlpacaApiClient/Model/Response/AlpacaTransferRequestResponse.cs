using Core;
using System;

namespace AlpacaApiClient.Model.Response
{
	public class AlpacaTransferRequestResponse
	{
		public Guid id { get; set; }
		public Guid relationship_id { get; set; }
		public Guid account_id { get; set; }
		public TransferTypeValue type { get; set; }
		public TransferStatusValue status { get; set; }
		public string reason { get; set; }
		public decimal amount { get; set; }
		public TransferDirectionValue direction { get; set; }
		public DateTime created_at { get; set; }
		public DateTime updated_at { get; set; }
		public DateTime expires_at { get; set; }
		public string additional_information { get; set; }
	}
}
