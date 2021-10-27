namespace Users.Core.Request
{
	public class OrderEntry
	{
		public string Ticker { get; set; }
		public decimal? DollarAmount { get; set; }
		public decimal? ShareAmount { get; set; }
	}
}
