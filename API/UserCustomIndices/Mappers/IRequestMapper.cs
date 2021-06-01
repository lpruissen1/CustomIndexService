using Database.Model.User.CustomIndices;
using System.Collections.Generic;
using UserCustomIndices.Core.Model.Requests;
using UserCustomIndices.Database.Model.User.CustomIndices;

namespace UserCustomIndices.Mappers
{
    public interface IRequestMapper
    {
        CustomIndex Map(CustomIndexRequest response);
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
                TimedRangeRule = MapTimedRangedRules(response.TimedRangeRule),

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

    }
}
