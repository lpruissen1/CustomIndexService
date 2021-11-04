using System;
using System.Collections.Generic;

namespace Users.Core.Response.Positions
{
	public class PortfolioPositions
	{
		public string Name { get; set; }
		public Guid PortfolioId { get; set; }
		public List<IndividualPosition> Positions { get; set; } = new List<IndividualPosition>();
	}
}
