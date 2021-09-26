using Microsoft.AspNetCore.Mvc;
using System;

namespace Users.Core
{
	public interface IPositionsService
	{
		public IActionResult GetAllPositions(Guid userId);
		public IActionResult GetPositionsForPortfolio(Guid userId, Guid portfolioId);
	}
}
