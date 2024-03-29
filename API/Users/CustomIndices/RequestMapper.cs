﻿using Core;
using System.Collections.Generic;
using System.Linq;
using Users.Core.Request;
using Users.Database.Model.CustomIndex;
using DB = Users.Database.Model.CustomIndex;
using RangedRule = Core.RangedRule;
using TimedRangeRule = Core.TimedRangeRule;

namespace Users.CustomIndices
{
	public class RequestMapper : IRequestMapper
    {
        public CustomIndex Map(CustomIndexRequest response)
        {
			return new CustomIndex()
			{
				UserId = response.UserId,
				IndexId = response.IndexId,
				Name = response.Name,
				Active = true,
				Markets = response.Markets,
				Sector = new DB.Sector { Sectors = response.Sectors, Industries = response.Industries },
				RangedRule = MapRangedRules(response.RangedRule),
				TimedRangeRule = MapTimedRangedRules(response.TimedRangeRule),
				Inclusions = response.Inclusions,
				Exclusions = response.Exclusions,
				WeightingOption = response.WeightingOption,
				ManualWeights = response.ManualWeights.ToDictionary(x => x.Ticker, y => y.Weight)
            };
        }

        public CustomIndex Map(CreateCustomIndexRequest response)
        {
            return new CustomIndex()
            {
				UserId = response.UserId,
				IndexId = response.IndexId,
				Name = response.Name,
				Active = true,
                Markets = response.Markets,
                Sector = new DB.Sector { Sectors = response.Sectors, Industries = response.Industries },
                RangedRule = MapRangedRules(response.RangedRule),
                TimedRangeRule = MapTimedRangedRules(response.TimedRangeRule),
                Inclusions = response.Inclusions,
                Exclusions = response.Exclusions,
				WeightingOption = response.WeightingOption,
				ManualWeights = response.ManualWeights.ToDictionary(x => x.Ticker, y => y.Weight)
			};
        }

        private List<DB.RangedRule> MapRangedRules(List<RangedRule> responseRangedRule)
        {
            var list = new List<DB.RangedRule>();

            foreach(var rule in responseRangedRule)
            {
                list.Add(new DB.RangedRule { RuleType = rule.RuleType, Ranges = new List<Range> { CreateRange(rule) } });
            }

            return list;
        }

        private List<DB.TimedRangeRule> MapTimedRangedRules(List<TimedRangeRule> reponseTimedRangedRules)
        {
            var list = new List<DB.TimedRangeRule>();

            foreach(var rule in reponseTimedRangedRules)
            {
                list.Add(new DB.TimedRangeRule { RuleType = rule.RuleType, TimedRanges = new List<DB.TimedRange> { CreateTimedRange(rule) } });
            }

            return list;
        }

        private DB.Range CreateRange(RangedRule responseRangeRule)
        {
            return new DB.Range { Upper = responseRangeRule.Upper, Lower = responseRangeRule.Lower };
        } 

        private DB.TimedRange CreateTimedRange(TimedRangeRule responseTimedRangeRule)
        {
            return new DB.TimedRange { Upper = responseTimedRangeRule.Upper, Lower = responseTimedRangeRule.Lower, TimePeriod = responseTimedRangeRule.TimePeriod };
        }
	}
}
