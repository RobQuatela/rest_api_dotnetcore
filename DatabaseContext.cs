using Microsoft.Extensions.Options;
using rest_api_dotnetcore.Config;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using rest_api_dotnetcore.Models;

namespace rest_api_dotnetcore
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if(client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Burger> Burger
        {
            get
            {
                return _database.GetCollection<Burger>("Burger");
            }
        }

        public IMongoCollection<User> User
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }

        public IMongoCollection<Role> Role
        {
            get
            {
                return _database.GetCollection<Role>("Role");
            }
        }

        public IMongoCollection<UserRole> UserRole
        {
            get
            {
                return _database.GetCollection<UserRole>("UserRole");
            }
        }
    }
}