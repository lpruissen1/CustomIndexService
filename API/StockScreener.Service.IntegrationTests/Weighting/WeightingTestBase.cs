using Core;
using NUnit.Framework;
using StockScreener.Core.Request;
using System.Collections.Generic;

namespace StockScreener.Service.IntegrationTests.Weighting
{
	public class WeightingTestBase : StockScreenerServiceTestBase
	{
		protected WeightingRequest weightingRequest;

		[SetUp]
		public override void SetUp()
		{
			base.SetUp();
			weightingRequest = new WeightingRequest();
		}

		public void AddTicker(string ticker)
		{
			weightingRequest.Tickers.Add(ticker);
		}

		public void AddManualTicker(string ticker, decimal weight)
		{
			weightingRequest.ManualWeights.Add(ticker, weight);
		}
	}
}
