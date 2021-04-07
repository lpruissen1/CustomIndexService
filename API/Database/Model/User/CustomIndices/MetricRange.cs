using Core;

namespace Database.Model.User.CustomIndices
{
    public class MetricRange
    {
        public double Upper;
        public double Lower;
    }

    public class TimedRangeMetric : MetricRange
    {
        public TimePeriod TimePeriod;
    }
}