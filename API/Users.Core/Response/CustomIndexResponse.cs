using Core;
using System.Collections.Generic;

namespace Users.Core.Response
{
	public class CustomIndexResponse
	{
		public string UserId { get; set; }
		public string IndexId { get; set; }
		public string Name { get; set; }
		public List<string> Markets { get; set; }
		public List<string> Sectors { get; init; } = new List<string>();
		public List<string> Industries { get; init; } = new List<string>();
		public List<TimedRangeRule> TimedRangeRule { get; init; } = new List<TimedRangeRule>();
		public List<RangedRule> RangedRule { get; init; } = new List<RangedRule>();
		public List<string> Inclusions { get; init; } = new List<string>();
		public List<string> Exclusions { get; init; } = new List<string>();
		public List<(string Ticker, decimal Weight)> ManualWeights { get; init; } = new List<(string Ticker, decimal Weight)>();
		public WeightingOption WeightingOption { get; init; }
		public RebalancingRules RebalancingFrequency { get; init; }
	}
}
