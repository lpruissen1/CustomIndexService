using AlpacaApiClient.Model.Response;
using Core;
using Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Users.Core.Response;
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
				FilledQuantity = response?.filled_qty,
				FilledAmount = response?.filled_qty * response?.filled_avg_price
			};
		}

		public static Order MapAlpacaOrderResponse(AlpacaOrderResponse response, Guid TransactionId)
		{
			return new Order
			{
				OrderId = response.id,
				PortfolioId = new Guid(),
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
				FilledQuantity = response?.filled_qty,
				FilledAmount = response?.filled_qty * response?.filled_avg_price
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

		public static AccountHistoryResponse MapAlpacaAccountHistoryResponse(AlpacaAccountHistoryResponse response)
		{
			var accountHistoryResponse =  new AccountHistoryResponse();

			for (int i = 0; i < response.timestamp.Length; i++)
			{
				accountHistoryResponse.AccountHistory.Add(response.timestamp[i], response.equity[i]);
			}

			return accountHistoryResponse;
		}
	}

	public static class ResponseMapper
	{
		public static AccountHistoryResponse MapAlpacaAccountHistoryResponse(AlpacaAccountHistoryResponse response, List<Transfer> transfers, DateTime firstTransfer)
		{
			var accountHistoryResponse =  new AccountHistoryResponse();

			var netContribution = 0m;

			for (int i = 0; i < response.timestamp.Length; i++)
			{
				var timestamp = response.timestamp[i];
				var timestampDatetime = DateTimeExtensions.UnixTimeStampToDateTime(timestamp);

				if (timestampDatetime <= firstTransfer && !timestampDatetime.SameDay(firstTransfer))
				{
					accountHistoryResponse.AccountHistory.Add(timestamp, 0);
				}
				else
				{
					accountHistoryResponse.AccountHistory.Add(response.timestamp[i], response.equity[i]);
				}

				// these must be in chronological order
				var currentTransfer = transfers.FirstOrDefault();

				if (currentTransfer is not null && timestampDatetime.SameDay(currentTransfer.Created))
				{
					netContribution += currentTransfer.Direction == TransferDirectionValue.INCOMING ? currentTransfer.Amount : -currentTransfer.Amount;
					transfers.Remove(currentTransfer);
				}

				accountHistoryResponse.NetContributions.Add(response.timestamp[i], netContribution);
			}

			return accountHistoryResponse;
		}
	}
}
