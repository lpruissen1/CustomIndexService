using Core;
using System;

namespace AlpacaApiClient.Model.Request
{
	public class AlpacaTransferRequest
	{
		public TransferTypeValue transfer_type { get; set; }
		public Guid relationship_id { get; set; }
		public string amount { get; set; }
		public TransferDirectionValue direction { get; set; }
	}
}
