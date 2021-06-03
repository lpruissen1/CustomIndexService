using Database.Model.User.CustomIndices;
using System.Collections.Generic;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Database.Model.User.CustomIndices;
using UserCustomIndices.Model.Response;

namespace UserCustomIndices.Mappers
{
    public interface IRequestMapper
    {
        CustomIndex Map(CustomIndexRequest response);
        CustomIndex Map(CreateCustomIndexRequest response);
		CustomIndexResponse Map(CustomIndex index);
	}

    public class RequestMapper : IRequestMapper
    {
        public CustomIndex Map(CustomIndexRequest response)
        {
            return new CustomIndex()
            {
				UserId = response.UserId,
                Markets = response.Markets,
                Sector = new Sector { Sectors = response.Sectors, Industries = response.Industries },
                RangedRule = MapRangedRules(response.RangedRule),
                TimedRangeRule = MapTimedRangedRules(response.TimedRangeRule)
            };
        }

        public CustomIndex Map(CreateCustomIndexRequest response)
        {
            return new CustomIndex()
            {
				UserId = response.UserId,
				IndexId = System.Guid.NewGuid().ToString(),
                Markets = response.Markets,
                Sector = new Sector { Sectors = response.Sectors, Industries = response.Industries },
                RangedRule = MapRangedRules(response.RangedRule),
                TimedRangeRule = MapTimedRangedRules(response.TimedRangeRule)
            };
        }

        private List<RangedRule> MapRangedRules(List<Core.Model.RangedRule> responseRangedRule)
        {
            var list = new List<RangedRule>();

            foreach(var rule in responseRangedRule)
            {
                list.Add(new RangedRule {RuleType = rule.RuleType, Ranges = new List<Range> { CreateRange(rule) } });
            }

            return list;
        }

        private List<TimedRangeRule> MapTimedRangedRules(List<Core.Model.TimedRangeRule> reponseTimedRangedRules)
        {
            var list = new List<TimedRangeRule>();

            foreach(var rule in reponseTimedRangedRules)
            {
                list.Add(new TimedRangeRule { RuleType = rule.RuleType, TimedRanges = new List<TimedRange> { CreateTimedRange(rule) } });
            }

            return list;
        }

        private Range CreateRange(Core.Model.RangedRule responseRangeRule)
        {
            return new Range { Upper = responseRangeRule.Upper, Lower = responseRangeRule.Lower };
        } 

        private TimedRange CreateTimedRange(Core.Model.TimedRangeRule responseTimedRangeRule)
        {
            return new TimedRange { Upper = responseTimedRangeRule.Upper, Lower = responseTimedRangeRule.Lower, TimePeriod = responseTimedRangeRule.TimePeriod };
        }

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

		private static List<Core.Model.RangedRule> MapRangedRule(List<Database.Model.User.CustomIndices.RangedRule> rangedRule)
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
