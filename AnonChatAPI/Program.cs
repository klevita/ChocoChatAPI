using AnonChatAPI.Models;
using AnonChatAPI.Services;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:8081/",
                                              "http://localhost:8081");
                      });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<mushroomsDBSettings>(
    builder.Configuration.GetSection("mushrooms")
    );
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ForumService>();
builder.Services.AddSingleton<MessageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);


app.MapGet("/PostMoney", () =>
{
    List<Coin> coins = new List<Coin>();

    coins.Add(new Coin());
    coins.Add(new Coin());
    coins.Add(new Coin());
    coins.Add(new Coin());
    coins.Add(new Coin());
    coins.Add(new Coin());

    coins[0].CoinName = "coin1";
    coins[1].CoinName = "coin121412";
    coins[2].CoinName = "coin3";
    coins[3].CoinName = "coin4";
    coins[4].CoinName = "coin5";
    coins[5].CoinName = "coin555555555555555 5";

    coins[0].CoinValue = 100;
    coins[1].CoinValue = 200;
    coins[2].CoinValue = 4000;
    coins[3].CoinValue = 50000000;
    coins[4].CoinValue = 3;
    coins[5].CoinValue = 0;

    coins[0].CoinTime = "22:21 04/18/2022";
    coins[1].CoinTime = "22:21 04/18/2022";
    coins[2].CoinTime = "22:21 04/18/2022";
    coins[3].CoinTime = "22:21 04/18/2022";
    coins[4].CoinTime = "22:21 04/18/2022";
    coins[5].CoinTime = "22:21 04/18/2022";

    coins[0].CoinId = 1;
    coins[1].CoinId = 2;
    coins[2].CoinId = 3;
    coins[3].CoinId = 4;
    coins[4].CoinId = 5;
    coins[5].CoinId = 6;
    return coins;
});

app.Run();


public class Coin : IEquatable<Coin>
{
    public string CoinName { get; set; }

    public uint CoinValue { get; set; }

    public string CoinTime { get; set; }

    public int CoinId { get; set; }

    public override string ToString()
    {
        return "ID: " + CoinId + "   Name: " + CoinName;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        Coin objAsPart = obj as Coin;
        if (objAsPart == null) return false;
        else return Equals(objAsPart);
    }
    public override int GetHashCode()
    {
        return CoinId;
    }
    public bool Equals(Coin other)
    {
        if (other == null) return false;
        return (this.CoinId.Equals(other.CoinId));
    }

    public class User
    {
        public bool IsAdmin { get; set; }
        public string NickName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string Date { get; set; } = null!;
    } }

// Добавить функции
//+ CreateUser(email, password, NickName) return (true\"error message")
//+ GetUser(email, password) return (User)
//getUserPosts(userID) return ([Messages])
//+ getUserForums(userID) return ([Forums])
//getAllForums() return ([Forums])
//getForumsByTag(TagName) return ([Forums])