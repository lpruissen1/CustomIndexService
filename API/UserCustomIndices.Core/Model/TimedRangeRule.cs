using Core;

namespace UserCustomIndices.Core.Model
{
    public class TimedRangeRule
    {
        public RuleType RuleType { get; set; }
        public double Upper { get; set; }
        public double Lower { get; set; }
        public TimePeriod TimePeriod { get; set; }
    }
}
