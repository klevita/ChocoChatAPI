using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AnonChatAPI.Models
{
    public class UserRegistration
    {
        [BsonElement("NickName")]
        public string NickName { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]

        [BsonElement("Email")]
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = null!;      
    }
}
