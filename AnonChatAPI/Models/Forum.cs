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

        [BsonElement("Date")]
        public string Date { get; set; } = null!;

        [BsonElement("Description")]
        public string Description { get; set; } = null!;

        [BsonElement("Tags")]
        public List<string> Tags { get; set; } = null!;
        //public string[] Tags { get; set; } = null!;

        [BsonElement("Creator")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Creator { get; set; } = null!;
    }
}
