using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnonChatAPI.Models    
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string NickName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
