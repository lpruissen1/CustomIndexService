using Database.Core;
using System.Collections.Generic;
using UserCustomIndices.Core;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class RangedRule : DbEntity
    {
        public RuleType RuleType;
        public List<Range> Ranges;
    }
}