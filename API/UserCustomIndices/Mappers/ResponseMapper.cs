using Core;
using Database.Model.User.CustomIndices;
using System.Collections.Generic;
using System.Linq;
using UserCustomIndices.Model.Response;
using DB = UserCustomIndices.Database.Model.User.CustomIndices;

namespace UserCustomIndices.Mappers
{
	public class ResponseMapper : IResponseMapper
    {
		public CustomIndexResponse Map(CustomIndex index)
		{
			return new CustomIndexResponse
			{
				UserId = index.UserId,
				IndexId = index.IndexId,
				Name = index.Name,
				Markets = index.Markets,
				Sectors = index.Sector.Sectors,
				Industries = index.Sector.Industries,
				TimedRangeRule = MapTimedRangeRule(index.TimedRangeRule),
				RangedRule = MapRangedRule(index.RangedRule),
				Inclusions = index.Inclusions,
				Exclusions = index.Exclusions,
				WeightingOption = index.WeightingOption,
				ManualWeights = index.ManualWeights.Select(kvp => (kvp.Key, kvp.Value)).ToList()
			};
		}

		private static List<TimedRangeRule> MapTimedRangeRule(List<DB.TimedRangeRule> timedRangeRule)
		{
			var requestRules = new List<TimedRangeRule>();

			foreach (var rule in timedRangeRule)
			{
				requestRules.Add(
					new TimedRangeRule
					{
						RuleType = rule.RuleType,
						Upper = rule.TimedRanges[0].Upper,
						Lower = rule.TimedRanges[0].Lower,
						TimePeriod = rule.TimedRanges[0].TimePeriod
					});
			}

			return requestRules;
		}

		private static List<RangedRule> MapRangedRule(List<DB.RangedRule> rangedRule)
		{
			var requestRules = new List<RangedRule>();

			foreach (var rule in rangedRule)
			{
				requestRules.Add(
					new RangedRule
					{
						RuleType = rule.RuleType,
						Upper = rule.Ranges[0].Upper,
						Lower = rule.Ranges[0].Lower,
					});
			}

			return requestRules;
		}
	}
}
