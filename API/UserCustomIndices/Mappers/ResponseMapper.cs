using Database.Model.User.CustomIndices;
using System.Collections.Generic;
using UserCustomIndices.Database.Model.User.CustomIndices;
using UserCustomIndices.Model.Response;

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
				Markets = index.Markets,
				Sectors = index.Sector.Sectors,
				Industries = index.Sector.Industries,
				TimedRangeRule = MapTimedRangeRule(index.TimedRangeRule),
				RangedRule = MapRangedRule(index.RangedRule)
			};
		}

		private static List<Core.Model.TimedRangeRule> MapTimedRangeRule(List<TimedRangeRule> timedRangeRule)
		{
			var requestRules = new List<Core.Model.TimedRangeRule>();

			foreach (var rule in timedRangeRule)
			{
				requestRules.Add(
					new Core.Model.TimedRangeRule
					{
						RuleType = rule.RuleType,
						Upper = rule.TimedRanges[0].Upper,
						Lower = rule.TimedRanges[0].Lower,
						TimePeriod = rule.TimedRanges[0].TimePeriod
					});
			}

			return requestRules;
		}

		private static List<Core.Model.RangedRule> MapRangedRule(List<RangedRule> rangedRule)
		{
			var requestRules = new List<Core.Model.RangedRule>();

			foreach (var rule in rangedRule)
			{
				requestRules.Add(
					new Core.Model.RangedRule
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
