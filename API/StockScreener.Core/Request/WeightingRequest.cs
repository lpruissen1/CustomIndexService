using System.Collections.Generic;

namespace StockScreener.Core.Request
{
	public class WeightingRequest
	{
		public WeightingOption Option { get; set; }
		public List<string> Tickers { get; set; } = new List<string>();
		public Dictionary<string, decimal> ManualWeights { get; set; } = new Dictionary<string, decimal>();
	}
}
