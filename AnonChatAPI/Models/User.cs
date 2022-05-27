using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AnonChatAPI.Models    
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("IsAdmin")]

        public bool IsAdmin { get; set; }
        public string NickName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Date { get; set; } = null!;
    }
}
