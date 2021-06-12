using Core;
using System.Collections.Generic;

namespace StockScreener.Core.Request
{
	public class ScreeningRequest
	{
		public List<string> Markets { get; set; } = new List<string>();
		public List<string> Sectors { get; set; } = new List<string>();
		public List<string> Industries { get; set; } = new List<string>();
		public List<TimedRangeRule> TimedRangeRule { get; set; } = new List<TimedRangeRule>();
		public List<RangedRule> RangedRule { get; set; } = new List<RangedRule>();
	}
}
