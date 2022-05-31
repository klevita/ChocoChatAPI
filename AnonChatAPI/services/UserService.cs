
using AnonChatAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace AnonChatAPI.Services
{

    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(
            IOptions<mushroomsDBSettings> userStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                userStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                userStoreDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(
                userStoreDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _usersCollection.Find(x => x.Id == id ).FirstOrDefaultAsync();
        public async Task<User?> GetAsync2(string name) =>
            await _usersCollection.Find(x => x.NickName == name).FirstOrDefaultAsync();
        public async Task<User?> GetAsync3(string email, string password) =>
			await _usersCollection.Find(x => x.Email == email && x.Password == password).FirstOrDefaultAsync(); 

        public async Task<User?> CreateAsync(UserRegistration newUser)
		{
			User abuser = await _usersCollection.Find(user => user.NickName == newUser.NickName || user.Email == newUser.Email).FirstOrDefaultAsync();
			if (abuser!=null)			
                if(abuser.NickName == newUser.NickName || abuser.Email == newUser.Email)
				    return null;			                       
			User _newUser = new User();
            _newUser.NickName = newUser.NickName;
            _newUser.Email = newUser.Email;
            _newUser.Password = newUser.Password;
            _newUser.Date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            _newUser.IsAdmin = false;           
            await _usersCollection.InsertOneAsync(_newUser);
            User created = await _usersCollection.Find(x => x.NickName == _newUser.NickName).FirstOrDefaultAsync();
            _newUser.Id = created.Id; 
            return _newUser;
        }

            

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
