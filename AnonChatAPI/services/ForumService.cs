using AnonChatAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnonChatAPI.Services
{
	public class ForumService
	{
		private readonly IMongoCollection<Forum> _forumsCollection;
		public ForumService(
			IOptions<mushroomsDBSettings> forumStoreDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				forumStoreDatabaseSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(
				forumStoreDatabaseSettings.Value.DatabaseName);
			_forumsCollection = mongoDatabase.GetCollection<Forum>(
				forumStoreDatabaseSettings.Value.ForumsCollectionName);
		}
		public async Task<List<Forum>> GetAsync() =>
		   await _forumsCollection.Find(_ => true).ToListAsync();
		public async Task<List<Forum>> GetAsync(string id) =>
			await _forumsCollection.Find(x => x.Creator == id).ToListAsync();
		public async Task<List<Forum>> GetAsync2(string tag) =>
			await _forumsCollection.Find(x => x.Tag == tag).ToListAsync();
		public async Task<Forum?> GetAsync3(string name) =>
			await _forumsCollection.Find(x => x.Name == name).FirstOrDefaultAsync();
	}
}
