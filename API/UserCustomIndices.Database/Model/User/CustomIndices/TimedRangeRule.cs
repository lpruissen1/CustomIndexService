using Database.Core;
using System.Collections.Generic;
using UserCustomIndices.Core;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class TimedRangeRule : DbEntity 
    {
        public RuleType RuleType;
        public List<TimedRange> TimedRanges;
    }
}