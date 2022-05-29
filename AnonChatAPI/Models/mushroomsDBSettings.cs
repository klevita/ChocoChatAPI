namespace AnonChatAPI.Models
{
    public class mushroomsDBSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
        public string ForumsCollectionName { get; set; } = null!;
        public string TagsCollectionName { get; set; } = null!;
        public string MessagesCollectionName { get; set; } = null!;

    }
}
