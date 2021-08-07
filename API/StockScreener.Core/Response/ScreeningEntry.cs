using Core;
using System.Collections.Generic;

namespace StockScreener.Core.Response
{
	public class ScreeningEntry
	{
		public ScreeningEntry(string ticker)
		{
			Ticker = ticker;
		}

		public string Ticker { get; set; }
		public string Name { get; set; }
		public string Sector { get; set; }
		public string Industry { get; set; }
		public double MarketCap { get; set; }
		public double CurrentPrice { get; set; }
		public List<ScreeningParamValue> ScreeningParameterValues { get; set; } = new List<ScreeningParamValue>();
	}

	public class ScreeningParamValue
	{
		public RuleType RuleType { get; set; }
		public TimePeriod? TimePeriod { get; set; }
		public double Value { get; set; }
	}
}
