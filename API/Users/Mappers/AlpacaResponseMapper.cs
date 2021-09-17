using AlpacaApiClient.Model.Response;
using System;
using Users.Database.Model;

namespace Users.Mappers
{
	public static class AlpacaResponseMapper
	{
		public static Order MapAlpacaOrderResponse(AlpacaOrderResponse response, Guid TransactionId)
		{
			return new Order
			{
				OrderId = response.id,
				TransactionId = TransactionId,
				Ticker = response.symbol,
				Status = response.status,
				Type = response.type,
				Side = response.side,
				Time_in_force = response.time_in_force,
				OrderedQuantity = response.qty,
				OrderedAmount = response.notional,
				FilledQuantity = null,
				FilledAmount = null
			};
		}

		public static Transfer MapAlpacaTransferResponse(AlpacaTransferRequestResponse response)
		{
			return new Transfer
			{
				AccountId = response.account_id,
				TransferId = response.id,
				Amount = response.amount,
				Status = response.status,
				TransferType = response.type,
				Direction = response.direction,
				Created = response.created_at
			};
		}
	}
}
