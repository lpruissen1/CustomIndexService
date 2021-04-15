using StockScreener.Model.Metrics;

namespace StockScreener.Mapper
{
    public interface IMetricListMapper<TInput>
    {
        MetricList MapToMetricList(TInput input);
    }
}
