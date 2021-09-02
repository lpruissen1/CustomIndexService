using System;
using System.Text.Json.Serialization;

namespace AlpacaApiClient.Model.Response
{
	public class AssetResponse
	{
		public Guid id { get; set; }

		[JsonPropertyName("class")]
		public string assetclass { get; set; }
		public string symbol { get; set; }
		public string name { get; set; }
		public string status { get; set; }
		public bool tradable { get; set; }
		public bool margin { get; set; }
		public bool shortable { get; set; }
		public bool easy_to_borrow { get; set; }
		public bool fractionable { get; set; }
	}
}
