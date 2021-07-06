namespace StockScreener.Core.Response
{
	public class ScreeningEntry
	{
		public ScreeningEntry(string ticker)
		{
			this.ticker = ticker;
		}

		public string ticker { get; set; }
	}
}
