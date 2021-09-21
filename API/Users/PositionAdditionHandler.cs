using System;
using System.Linq;
using Users.Database.Model;
using Users.Database.Repositories.Interfaces;

namespace Users
{
	public class PositionAdditionHandler : IPositionAdditionHandler
	{
		public PositionAdditionHandler(IUserPositionsRepository userPositionsRepository)
		{
			this.userPositionsRepository = userPositionsRepository;
		}

		private IUserPositionsRepository userPositionsRepository { get; }

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
				userPositionsRepository.UpdatePosition(userId, existingPosition);
				return;
			}

			existingPosition.Portfolios.Add(portfolioId, newPurchaseQuantity);

			userPositionsRepository.UpdatePosition(userId, existingPosition);
		}
	}

	public interface IPositionAdditionHandler
	{
		public void AddPosition(Guid userId, Position position);
	}
}
