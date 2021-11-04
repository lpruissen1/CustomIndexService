using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Users.Core.Response.Positions;

namespace Users.Core
{
	public interface IPositionsService
	{
		IActionResult GetAllPositions(Guid userId);
		IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId);
		Task<PositionsResponse> GetPortfolios(Guid userId);
	}
}
