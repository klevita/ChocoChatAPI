using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AnonChatAPI.Models
{
    public class ForumCreation
    {
        [BsonElement("Name")]
        public string Name { get; set; } = null!;

        [BsonElement("Description")]
        public string Description { get; set; } = null!;

        [BsonElement("Tags")]
        public List<string> Tags { get; set; } = null!;        

        [BsonElement("Creator")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Creator { get; set; } = null!;
    }
}
