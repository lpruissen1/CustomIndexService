using System.Collections.Generic;

namespace ApiClient.Models.Eod
{
	public class EodExchangeEntry
	{
		public string Code { get; set; }
	}
	public class EodIndex
	{
		public Dictionary<int, EodExchangeEntry> Components { get; set; }
	}
}
