using Core;
using NUnit.Framework;
using StockScreener.Core;
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

		public void AddManualTicker(string ticker, double weight)
		{
			weightingRequest.ManualWeights.Add(new WeightingEntry(ticker, weight));
		}
	}
}
