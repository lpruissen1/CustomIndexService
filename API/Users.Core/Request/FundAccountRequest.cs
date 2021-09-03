using Core;

namespace Users.Core.Request
{
	public class FundAccountRequest
	{
		public string TransferType { get; set; }
		public string RelationshipId { get; set; }
		public string Amount { get; set; }
		public TransferDirectionValue Direction { get; set; }
	}
}
