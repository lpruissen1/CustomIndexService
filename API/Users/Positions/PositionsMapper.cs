using AlpacaApiClient.Model.Response;
using System;
using System.Linq;
using Users.Core.Response.Positions;
using Users.Database.Model;

namespace Users.Positions
{
	public static class PositionsMapper
	{
		public static PositionsResponse MapPositions(AlpacaPositionResponse[] response, UserPositions userPositions)
		{

			var portfolioIds = userPositions.Positions.SelectMany(x => x.Portfolios.Select(x => x.Key)).Distinct();

			var positionsResponse = new PositionsResponse();

			foreach (var portfolio in portfolioIds)
			{
				var portfolioHoldings = new PortfolioPositions();
				portfolioHoldings.PortfolioId = new Guid(portfolio);

				foreach (var position in userPositions.Positions)
				{
					if (position.Portfolios.TryGetValue(portfolio, out var quantity))
					{
						portfolioHoldings.Positions.Add(new IndividualPosition()
						{
							Ticker = position.Ticker,
							AveragePurchasePrice = position.AveragePurchasePrice,
							Quantity = quantity,
							CurrentPrice = response.First(x => x.symbol == position.Ticker).current_price
						});
					}
				}

				positionsResponse.Portfolios.Add(portfolioHoldings);
			}

			return positionsResponse;
		}
	}
}
