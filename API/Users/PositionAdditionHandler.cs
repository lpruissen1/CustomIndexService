using AlpacaApiClient.Model.Response.Events;
using Microsoft.Extensions.Logging;
using System.Linq;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;
using Users.Mappers;

namespace Users
{
	public class PositionAdditionHandler : IPositionAdditionHandler
	{
		public PositionAdditionHandler(ILogger logger, IUserAccountsRepository userAccountsRepository, IUserOrdersRepository userOrdersRepository, IUserPositionsRepository userPositionsRepository)
		{
			this.logger = logger;
			this.userAccountsRepository = userAccountsRepository;
			this.userOrdersRepository = userOrdersRepository;
			this.userPositionsRepository = userPositionsRepository;
		}

		private ILogger logger { get; }
		private IUserAccountsRepository userAccountsRepository { get; }
		private IUserOrdersRepository userOrdersRepository { get; }
		private IUserPositionsRepository userPositionsRepository { get; }

		public void AddPosition(TradeEvent tradeEvent)
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
	}

	public interface IPositionAdditionHandler
	{
		public void AddPosition(TradeEvent tradeEvent);
	}
}
