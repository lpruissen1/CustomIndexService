using Core;
using System;

namespace AlpacaApiClient.Model.Response.Events
{
	public class TransferEvent
	{
		public Guid account_id { get; set; }
		public DateTime at { get; set; }
		public int event_Id{ get; set; }
		public TransferStatusValue status_from { get; set; }
		public TransferStatusValue status_to { get; set; }
		public Guid transfer_id{ get; set; }
	}
}
