using System.Collections.Generic;

namespace ApiClient.Models
{
	public class PolygonPriceDataResponse
	{
        public string Ticker { get; set; }
		public List<PolygonCandles> Results { get; set; }
	}
}
