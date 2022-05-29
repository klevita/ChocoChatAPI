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
		public async Task<Message?> GetAsync1(string id) =>
			await _messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task<List<Message>> GetAsync2(string id) =>
			await _messagesCollection.Find(x => x.MessageU == id).ToListAsync();
		public async Task<List<Message>> GetAsync3(string id) =>
			await _messagesCollection.Find(x => x.MessageF == id).ToListAsync();
		public long GetAsync4(string id) =>
			_messagesCollection.CountDocuments(x => x.MessageF == id);


		public async Task CreateAsync(Message newM)
		{
			//Message _newM = new Message();
			//_newUser.Id = created.Id;
			//return _newUser;
			await _messagesCollection.InsertOneAsync(newM);
		}

		public async Task UpdateAsync(string id, Message updatedM) =>
		   await _messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedM);
		public async Task RemoveAsync(string id) =>
			await _messagesCollection.DeleteOneAsync(x => x.Id == id);
	}
}