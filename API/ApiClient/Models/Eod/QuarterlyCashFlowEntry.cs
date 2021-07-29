using System;

namespace ApiClient.Models.Eod
{
	public class QuarterlyCashFlowEntry
	{
		public DateTime? filing_date { get; set; }
		public double? dividendsPaid { get; set; }
		public double? freeCashFlow { get; set; }
	}
}
