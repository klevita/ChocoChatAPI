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
		public async Task<Forum?> GetAsync1(string id) =>
			await _forumsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
		public async Task<List<Forum>> GetAsync2(string tag)
		{
			List<Forum> listR = new List<Forum>();		
			var list = await _forumsCollection.Find(_ => true).ToListAsync();			
			foreach (var item in list)
			{
				foreach (var _tag in item.Tags)
				{
					if (tag == _tag)
					{
						listR.Add(item);
					}
				}
			}
			return listR;	
		}


		public async Task<List<Forum>> GetAsync3(string name) =>
			await _forumsCollection.Find(x => x.Name == name).ToListAsync();

		public async Task<Forum> CreateAsync(ForumCreation newF)
		{
			Forum ex = await _forumsCollection.Find(f => f.Name == newF.Name).FirstOrDefaultAsync();
			if (ex != null)
				if (ex.Name == newF.Name)
					return null;
			Forum newForum = new Forum();
			newForum.Creator = newF.Creator;
			newForum.Name = newF.Name;
			newForum.Tags = newF.Tags;
			newForum.Description = newF.Description;
			newForum.Date = DateTime.UtcNow.ToString("MM-dd-yyyy");
			await _forumsCollection.InsertOneAsync(newForum);
			return newForum;
		}			
		public async Task UpdateAsync(string id, Forum updatedF) =>
		   await _forumsCollection.ReplaceOneAsync(x => x.Id == id, updatedF);
		public async Task RemoveAsync(string id) =>
			await _forumsCollection.DeleteOneAsync(x => x.Id == id);
	}
}
