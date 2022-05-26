using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnonChatAPI.Models
{
	public class Message
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		[BsonElement("Name")]
		public string MessageF { get; set; } = null!;

		public string Content { get; set; } = null!;

		public string Date { get; set; } = null!;

		public string MessageU { get; set; } = null!;
	}
}
