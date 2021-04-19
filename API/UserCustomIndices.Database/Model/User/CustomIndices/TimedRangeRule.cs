using System.Collections.Generic;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class TimedRangeRule : CustomIndexRule 
    { 
        public List<TimedRange> TimedRanges;
    }
}