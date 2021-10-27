using AlpacaApiClient.Model.Response.Events;
using Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;
using Users.Mappers;

namespace Users
{
	public class PositionUpdateHandler : IPositionAdditionHandler
	{
		public PositionUpdateHandler(ILogger logger, IUserAccountsRepository userAccountsRepository, IUserOrdersRepository userOrdersRepository, IUserPositionsRepository userPositionsRepository)
		{
			this.userPositionsRepository = userPositionsRepository;
			this.logger = logger;
			this.userAccountsRepository = userAccountsRepository;
			this.userOrdersRepository = userOrdersRepository;
		}

		private IUserPositionsRepository userPositionsRepository { get; }
		private ILogger logger { get; }
		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserOrdersRepository userOrdersRepository { get; }

		// does not handle the fact there could be addition to an existing position.
		public void AddPosition(Guid userId, Position position)
		{
			var existingPosition = userPositionsRepository.GetByUserId(userId).Positions.FirstOrDefault(x => x.Ticker == position.Ticker);

			if(existingPosition is null)
			{
				userPositionsRepository.CreatePosition(userId, position);
				return;
			}

			var portfolioId = position.Portfolios.First().Key;
			var currentAveragePrice = existingPosition.AveragePurchasePrice;
			var currentQuantity = existingPosition.Portfolios.Sum(x => x.Value);
			var newPurchaseQuantity = position.Portfolios.First().Value;
			var newAveragePrice = ((currentAveragePrice * currentQuantity) + (position.AveragePurchasePrice * newPurchaseQuantity)) / (currentQuantity + newPurchaseQuantity);

			existingPosition.AveragePurchasePrice = newAveragePrice;

			if (existingPosition.Portfolios.ContainsKey(portfolioId))
			{
				existingPosition.Portfolios[portfolioId] += newPurchaseQuantity;
				userPositionsRepository.UpdatePositionAveragePurchacePrice(userId, existingPosition);
				return;
			}

			existingPosition.Portfolios.Add(portfolioId, newPurchaseQuantity);

			userPositionsRepository.UpdatePositionAveragePurchacePrice(userId, existingPosition);
		}

		public void HandlePositionUpdate(TradeEvent tradeEvent)
		{
			var relatedUser = userAccountsRepository.GetByAccountId(tradeEvent.account_id).UserId;

			if (tradeEvent.order.side == OrderDirectionValue.buy)
			{
				var filledOrder = AlpacaResponseMapper.MapAlpacaOrderResponse(tradeEvent.order);
				var userOrders = userOrdersRepository.GetByUserId(relatedUser).Orders;
				var relatedOrder = userOrders.FirstOrDefault(x => x.OrderId == filledOrder.OrderId);
				if (relatedOrder is not null)
				{
					userOrdersRepository.FillOrder(relatedUser, relatedOrder.OrderId, filledOrder);
					logger.LogInformation($"Filled order for accout, {tradeEvent.account_id}, order for {tradeEvent.order.symbol}");
					var newPosition = new Position(tradeEvent.order.symbol, tradeEvent.order.filled_avg_price.Value, relatedOrder.PortfolioId, tradeEvent.order.filled_qty);

					AddPosition(relatedUser, newPosition);
				}
				else
				{
					logger.LogInformation(new EventId(1), "Failed to find order");
				}
				return;
			}
			else if (tradeEvent.order.side == OrderDirectionValue.sell)
			{
				var userOrders = userOrdersRepository.GetByUserId(relatedUser).Orders;
				var relatedOrder = userOrders.FirstOrDefault(x => x.OrderId == tradeEvent.order.id);
				if (relatedOrder is not null)
				{
					ReducePosition(relatedUser, relatedOrder.PortfolioId, tradeEvent.order.symbol, tradeEvent.order.filled_qty);

				}
				// if buy
				// handle buying update

				// if sell
				// handle selling update
			}
		}

		public void ReducePosition(Guid userId, Guid portfolioId, string ticker, decimal amount)
		{
			var positions = userPositionsRepository.GetByUserId(userId);
			var position = positions.Positions.First(x => x.Ticker == ticker);

			if(position.Portfolios.Count == 1)
			{
				if (position.Portfolios.First().Value == amount)
				{
					userPositionsRepository.RemovePosition(userId, ticker);
				}
				else 
				{
					position.Portfolios[portfolioId.ToString()] = amount;
	
					positions.Positions.RemoveAll(x => x.Ticker == ticker);
					positions.Positions.Add(position);

					userPositionsRepository.Replace(userId, positions);
				}
			}
			else
			{

			}
			// determines which portfolio it belong to by looking at the current sell orders in our system
			// if position is reduced
				// update the quantity in the given portfolio
			// if position is removed
				// if position exists in multiple portfolios
					// remove portfilio from position
					// update avergae purchase price
				// if exists only in one portfolio
					// remove postion outright
			throw new NotImplementedException();
		}
	}

	public interface IPositionAdditionHandler
	{
		public void AddPosition(Guid userId, Position position);
		public void HandlePositionUpdate(TradeEvent tradeEvent);
	}
}
