using NUnit.Framework;

namespace StockScreener.Core.Test
{
    [TestFixture]
    public class GrowthRateTests
    {
        [Test]
        public void TestGrowthRate()
        {
            var present = 100;
            var past = 50;

            var result = GrowthRateCalculator.CalculateGrowthRate(present, past);

            Assert.AreEqual(100, result);
        }
    }
}
