using AlpacaApiClient.Model.Response;
using System;
using Users.Database.Model;

namespace Users.Mappers
{
	public static class AlpacaResponseMapper
	{
		public static Order MapAlpacaOrderResponse(AlpacaOrderResponse response, Guid TransactionId, Guid PortfolioId)
		{
			return new Order
			{
				OrderId = response.id,
				PortfolioId = PortfolioId,
				TransactionId = TransactionId,
				Ticker = response.symbol,
				Status = response.status,
				CreatedAt = response.created_at,
				FilledAt = response.filled_at.GetValueOrDefault(),
				Type = response.type,
				Side = response.side,
				Time_in_force = response.time_in_force,
				OrderedQuantity = response?.qty,
				OrderedAmount = response?.notional,
				FilledQuantity = null,
				FilledAmount = null
			};
		}

		public static Order MapAlpacaOrderResponse(AlpacaOrderResponse response)
		{
			return new Order
			{
				OrderId = response.id,
				Ticker = response.symbol,
				Status = response.status,
				CreatedAt = response.created_at,
				FilledAt = response.filled_at.Value,
				Type = response.type,
				Side = response.side,
				Time_in_force = response.time_in_force,
				OrderedQuantity = response.qty,
				OrderedAmount = response.notional,
				FilledQuantity = response.filled_qty,
				FilledAmount = response.filled_avg_price * response.filled_qty
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
