using Core;
using System.Collections.Generic;

namespace UserCustomIndices.Core.Model.Requests
{
	public class CustomIndexRequest
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
		public List<ManualWeightingEntry> ManualWeights { get; init; } = new List<ManualWeightingEntry>();
		public WeightingOption WeightingOption { get; init; }
	}

	public class ManualWeightingEntry
	{
		public string Ticker { get; set; }
		public decimal Weight { get; set; }
	}
}
