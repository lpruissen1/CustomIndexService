using System;
using System.Collections.Generic;

namespace Users.Database.Model
{
	public class Position
	{
		public Position(string ticker, decimal averagePurchasePrice, Guid portfolioId, decimal quantity)
		{
			Ticker = ticker;
			AveragePurchasePrice = averagePurchasePrice;
			Portfolios.Add(portfolioId.ToString(), quantity);
		}

		public string Ticker { get; set; }
		public decimal AveragePurchasePrice { get; set; }
		public Dictionary<string, decimal> Portfolios { get; set; } = new Dictionary<string, decimal>();
	}
}
