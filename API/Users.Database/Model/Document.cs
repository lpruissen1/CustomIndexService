using AlpacaApiClient.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.Database.Model
{
	public class Document
	{
		[BsonRepresentation(BsonType.String)]
		public DocumentTypeValue document_type { get; set; }
		public string document_sub_type { get; set; }
		public string content { get; set; }
		public string mime_type { get; set; }
	}
}
