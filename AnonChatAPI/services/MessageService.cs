using AnonChatAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnonChatAPI.Services
{
	public class MessageService
	{
		private readonly IMongoCollection<Message> _messagesCollection;
		public MessageService(
			IOptions<mushroomsDBSettings> messageStoreDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				messageStoreDatabaseSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(
				messageStoreDatabaseSettings.Value.DatabaseName);
			_messagesCollection = mongoDatabase.GetCollection<Message>(
				messageStoreDatabaseSettings.Value.MessagesCollectionName);
		}
		public async Task<List<Message>> GetAsync() =>
		   await _messagesCollection.Find(_ => true).ToListAsync();
		public async Task<List<Message>> GetAsync2(string id) =>
			await _messagesCollection.Find(x => x.MessageU == id).ToListAsync();
	}
}