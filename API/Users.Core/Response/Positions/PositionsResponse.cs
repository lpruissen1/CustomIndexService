using System.Collections.Generic;

namespace Users.Core.Response.Positions
{
	public class PositionsResponse
	{
		public List<PortfolioPositions> Portfolios { get; } = new List<PortfolioPositions>();
	}
}
