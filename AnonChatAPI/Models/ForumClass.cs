using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnonChatAPI.Models
{
	public class Forum
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]    
        public string Name { get; set; } = null!;

        public string Date { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Tags { get; set; } = null!;
        public string Creator { get; set; } = null!;
    }
}
