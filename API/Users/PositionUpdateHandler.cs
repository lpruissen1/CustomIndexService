using AlpacaApiClient;
using AlpacaApiClient.Model.Response.Events;
using Core;
using Microsoft.Extensions.Logging;
using System.Linq;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;
using Users.Mappers;

namespace Users
{
	public class PositionUpdateHandler : IPositionUpdateHandler
	{
		public PositionUpdateHandler(ILogger logger, IUserAccountsRepository userAccountsRepository, IUserOrdersRepository userOrdersRepository, IUserPositionsRepository userPositionsRepository)
		{
			this.logger = logger;
			this.userAccountsRepository = userAccountsRepository;
			this.userOrdersRepository = userOrdersRepository;
			this.userPositionsRepository = userPositionsRepository;
			this.alpacaClient = new AlpacaClient(new AlpacaApiSettings { Key = "CKXM3IU2N9VWGMI470HF", Secret = "ZuT1Jrbn9VFU1bt3egkjdyoOseWNCZ1c5pjYMH7H" }, logger);
		}

		private ILogger logger { get; }
		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserOrdersRepository userOrdersRepository { get; }
		private IUserPositionsRepository userPositionsRepository { get; }
		private AlpacaClient alpacaClient { get; }

		public void UpdatePosition(TradeEvent tradeEvent)
		{
			if (tradeEvent.order.side == OrderDirectionValue.buy) 
			{
				AddPosition(tradeEvent);
			}
			else
			{
				SellPosition(tradeEvent);
			}
		}

		private void AddPosition(TradeEvent tradeEvent)
		{
			var filledOrder = AlpacaResponseMapper.MapAlpacaOrderResponse(tradeEvent.order);
			var relatedUser = userAccountsRepository.GetByAccountId(tradeEvent.account_id).UserId;
			var userOrders = userOrdersRepository.GetByUserId(relatedUser).Orders;
			var relatedOrder = userOrders.FirstOrDefault(x => x.OrderId == filledOrder.OrderId);

			if (relatedOrder is not null)
			{
				userOrdersRepository.FillOrder(relatedUser, relatedOrder.OrderId, filledOrder);
				logger.LogInformation($"Filled order for accout, {tradeEvent.account_id}, order for {tradeEvent.order.symbol}");
				var newPosition = new Position(tradeEvent.order.symbol, tradeEvent.order.filled_avg_price.Value, relatedOrder.PortfolioId, tradeEvent.order.filled_qty);

				var existingPosition = userPositionsRepository.GetByUserId(relatedUser).Positions.FirstOrDefault(x => x.Ticker == newPosition.Ticker);

				if (existingPosition is null)
				{
					userPositionsRepository.CreatePosition(relatedUser, newPosition);
					return;
				}

				var portfolioId = newPosition.Portfolios.First().Key;
				var currentAveragePrice = existingPosition.AveragePurchasePrice;
				var currentQuantity = existingPosition.Portfolios.Sum(x => x.Value);
				var newPurchaseQuantity = newPosition.Portfolios.First().Value;
				var newAveragePrice = ((currentAveragePrice * currentQuantity) + (newPosition.AveragePurchasePrice * newPurchaseQuantity)) / (currentQuantity + newPurchaseQuantity);

				existingPosition.AveragePurchasePrice = newAveragePrice;

				if (existingPosition.Portfolios.ContainsKey(portfolioId))
				{
					existingPosition.Portfolios[portfolioId] += newPurchaseQuantity;
					userPositionsRepository.UpdatePosition(relatedUser, existingPosition);
					return;
				}

				existingPosition.Portfolios.Add(portfolioId, newPurchaseQuantity);

				userPositionsRepository.UpdatePosition(relatedUser, existingPosition);

			}
			else
			{
				// retry
				logger.LogInformation(new EventId(1), "Failed to find order");
			}

		}

		private void SellPosition(TradeEvent tradeEvent)
		{
			var filledOrder = AlpacaResponseMapper.MapAlpacaOrderResponse(tradeEvent.order);
			var relatedUser = userAccountsRepository.GetByAccountId(tradeEvent.account_id).UserId;
			var userOrders = userOrdersRepository.GetByUserId(relatedUser).Orders;
			var relatedOrder = userOrders.FirstOrDefault(x => x.OrderId == filledOrder.OrderId);

			if (relatedOrder is not null)
			{
				userOrdersRepository.FillOrder(relatedUser, relatedOrder.OrderId, filledOrder);
				logger.LogInformation($"Filled order for accout, {tradeEvent.account_id}, order for {tradeEvent.order.symbol}");

				var userPositions = userPositionsRepository.GetByUserId(relatedUser);
				var soldPosition = userPositions.Positions.First(x => x.Ticker == filledOrder.Ticker);

				var qty = soldPosition.Portfolios[relatedOrder.PortfolioId.ToString()];
				var soldQty = filledOrder.FilledQuantity;

				if(soldPosition.Portfolios.Count == 1)
				{
					if (qty == soldQty) 
					{
						userPositions.Positions.RemoveAll(x => x.Ticker == filledOrder.Ticker);
						userPositionsRepository.UpdatePosition(relatedUser, userPositions);
						return;
					}
					else
					{
						userPositions.Positions.First(x => x.Ticker == filledOrder.Ticker).Portfolios[relatedOrder.PortfolioId.ToString()] = (qty - soldQty.Value);
					}	
				}
				else
				{

					if (qty == soldQty)
					{
						userPositions.Positions.First(x => x.Ticker == filledOrder.Ticker).Portfolios.Remove(filledOrder.PortfolioId.ToString());
					}
					else
					{
						userPositions.Positions.First(x => x.Ticker == filledOrder.Ticker).Portfolios[relatedOrder.PortfolioId.ToString()] = (qty - soldQty.Value);

					}
				}

				var newAveragePurchasePrice = alpacaClient.GetPosition(tradeEvent.account_id, filledOrder.Ticker)?.avg_entry_price;
				userPositions.Positions.First(x => x.Ticker == filledOrder.Ticker).AveragePurchasePrice = newAveragePurchasePrice.Value;


				userPositionsRepository.UpdatePosition(relatedUser, userPositions);
			}
			else
			{
				// retry
				logger.LogInformation(new EventId(1), "Failed to find order");
			}

		}
	}

	public interface IPositionUpdateHandler
	{
		public void UpdatePosition(TradeEvent tradeEvent);
	}
}
