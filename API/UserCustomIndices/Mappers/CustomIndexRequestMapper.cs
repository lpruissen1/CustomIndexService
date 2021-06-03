using Database.Model.User.CustomIndices;
using System.Collections.Generic;
using UserCustomIndices.Core.Model;
using UserCustomIndices.Core.Model.Requests;

namespace UserCustomIndices.Mappers
{
	public class CustomIndexRequestMapper
	{
		public static CustomIndexRequest MapToCustomIndexRequest(CustomIndex index)
		{
			return new CustomIndexRequest
			{
				UserId = index.UserId,
				IndexId = index.IndexId,
				Markets = index.Markets,
				Sectors = index.Sector.Sectors,
				Industries = index.Sector.Industries,
				TimedRangeRule = MapTimedRangeRule(index.TimedRangeRule),
				RangedRule = MapRangedRule(index.RangedRule)
			};
		}


		private static List<TimedRangeRule> MapTimedRangeRule(List<Database.Model.User.CustomIndices.TimedRangeRule> timedRangeRule)
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

		private static List<RangedRule> MapRangedRule(List<Database.Model.User.CustomIndices.RangedRule> rangedRule)
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
