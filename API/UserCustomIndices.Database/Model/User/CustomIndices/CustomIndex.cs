using Database.Core;
using System.Collections.Generic;
using Core;
using UserCustomIndices.Database.Model.User.CustomIndices;
using RangedRule = UserCustomIndices.Database.Model.User.CustomIndices.RangedRule;
using TimedRangeRule = UserCustomIndices.Database.Model.User.CustomIndices.TimedRangeRule;

namespace Database.Model.User.CustomIndices
{
    public class CustomIndex : DbEntity
    {
        public string UserId { get; init; }
        public string IndexId { get; init; }
        public string Name { get; init; }
        public List<string> Markets { get; init; } = new List<string>();
        public Sector Sector { get; set; }
		public bool Active { get; set; }
        public List<TimedRangeRule> TimedRangeRule { get; init; } = new List<TimedRangeRule>();
        public List<RangedRule> RangedRule { get; init; } = new List<RangedRule>();
        public Dictionary<string, decimal> ManualWeights { get; init; } = new Dictionary<string, decimal>();
        public WeightingOption WeightingOption { get; init; }
    }
}
