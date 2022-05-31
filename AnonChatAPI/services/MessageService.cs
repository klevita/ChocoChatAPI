using AnonChatAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnonChatAPI.Services
{
	public class MessageService
	{
		private readonly IMongoCollection<Message> _messagesCollection;
		private readonly IMongoCollection<Message> _usersCollection;
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


		public async Task<Message> CreateAsync(MessageCreation newM)
		{
			//User abuser = await _usersCollection.Find(message => message).FirstOrDefaultAsync();
			//if (abuser != null)
			//	if (abuser.NickName == newUser.NickName || abuser.Email == newUser.Email)
			//		return null;
			Message _newM = new Message();			
			_newM.MessageU = newM.MessageU;
			_newM.MessageF = newM.MessageF;
			_newM.Content = newM.Content;
			_newM.Date = DateTime.UtcNow.ToString("MM-dd-yyyy");
			await _messagesCollection.InsertOneAsync(_newM);
			Message created = await _messagesCollection.Find(x => x.MessageF == _newM.MessageF && x.MessageU == _newM.MessageU && x.Content == _newM.Content).FirstOrDefaultAsync();
			_newM.Id = created.Id;
			return _newM;
		}

		public async Task UpdateAsync(string id, Message updatedM) =>
		   await _messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedM);
		public async Task RemoveAsync(string id) =>
			await _messagesCollection.DeleteOneAsync(x => x.Id == id);
	}
}