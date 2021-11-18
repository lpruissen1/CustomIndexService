using Core;
using Database.Core;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Users.Database.Model.CustomIndex
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
		public List<string> Inclusions { get; init; } = new List<string>();
		public List<string> Exclusions { get; init; } = new List<string>();
		public Dictionary<string, decimal> ManualWeights { get; init; } = new Dictionary<string, decimal>();
		[BsonRepresentation(BsonType.String)]
		public WeightingOption WeightingOption { get; init; }
	}
}
