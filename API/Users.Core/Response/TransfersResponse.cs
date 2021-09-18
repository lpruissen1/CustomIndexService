using Core;
using System;

namespace Users
{
	public class TransfersResponse
	{
		public Guid AccountId { get; set; }

		public Guid TransferId { get; set; }

		public decimal Amount { get; set; }

		public DateTime Created { get; set; }

		public TransferStatusValue Status { get; set; }

		public TransferDirectionValue Direction { get; set; }
	}
}
