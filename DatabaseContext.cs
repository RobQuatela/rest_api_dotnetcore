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
            //var client = new MongoClient("mongodb://localhost:27017");
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
    }
}