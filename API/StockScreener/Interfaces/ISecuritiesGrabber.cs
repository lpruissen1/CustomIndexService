using StockScreener.Model;

namespace StockScreener
{
    public interface ISecuritiesGrabber
    {
        SecuritiesList GetSecurities(MetricList metrics);
    }
}
