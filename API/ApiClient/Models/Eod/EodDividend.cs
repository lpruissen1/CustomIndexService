using System;

namespace ApiClient.Models.Eod
{
	public class EodDividend
	{
		public DateTime date { get; set; }
		public DateTime? recordDate { get; set; }
		public DateTime? paymentDate { get; set; }
		public double value { get; set; }
	}
}
