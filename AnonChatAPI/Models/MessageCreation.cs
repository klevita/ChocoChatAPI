using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnonChatAPI.Models
{
	public class MessageCreation
	{
		[BsonElement("MessageF")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string MessageF { get; set; } = null!;

		[BsonElement("Content")]
		public string Content { get; set; } = null!;

		[BsonElement("Date")]
		public string Date { get; set; } = null!;

		[BsonElement("MessageU")]
		[BsonRepresentation(BsonType.ObjectId)]
		public string MessageU { get; set; } = null!;
	}
}