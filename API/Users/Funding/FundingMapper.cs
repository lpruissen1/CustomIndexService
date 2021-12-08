using Users.Database.Model;

namespace Users.Funding
{
	public static class FundingMapper
	{
		public static TransfersResponse MapTransfer(Transfer transfer)
		{
			return new TransfersResponse()
			{
				AccountId = transfer.AccountId,
				TransferId = transfer.TransferId,
				Amount = transfer.Amount,
				Created = transfer.CreatedAt,
				Status = transfer.Status,
				Direction = transfer.Direction
			};
		}
	}
}
