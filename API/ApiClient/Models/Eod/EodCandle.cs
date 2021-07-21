using System;

namespace ApiClient.Models.Eod
{
	public class EodCandle
	{
		public DateTime date { get; set; }
		public double open { get; set; }
		public double high { get; set; }
		public double low { get; set; }
		public double close { get; set; }
	}
}
