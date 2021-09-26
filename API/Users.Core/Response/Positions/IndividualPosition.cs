namespace Users.Core.Response.Positions
{
	public class IndividualPosition
	{
		public string Ticker { get; set; }
		public decimal AveragePurchasePrice { get; set; }
		public decimal Quantity { get; set; }
		public decimal CurrentPrice { get; set; }
	}
}
