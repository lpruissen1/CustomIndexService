namespace StockScreener.Core
{
	public class WeightingEntry
	{
		public WeightingEntry(string ticker, double weight)
		{
			Ticker = ticker;
			Weight = weight;
		}

		public string Ticker { get; init; }
		public double Weight { get; set; }
	}
}
