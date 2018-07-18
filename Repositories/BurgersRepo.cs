using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using rest_api_dotnetcore.Models;

namespace rest_api_dotnetcore.Repositories
{
    public interface IBurgersRepo
    {
        // list all burgers from collection
        Task<IEnumerable<Burger>> ListAll();
        // find burger by id
        Task<Burger> FindById(string id);
        // find burger by name
        Task<Burger> FindByName(string name);
        // save burger to collection
        Task<Burger> Save(Burger burger);
        // update burger
        Task<Burger> Update(Burger burger);
        // remove burger
        Task Delete(string id);
    }

    public class BurgersRepo : IBurgersRepo
    {
        private readonly DatabaseContext _dbContext;

        public BurgersRepo(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(string id)
        {
            var burger = await FindById(id);

            // create filter definition to input into DeleteOne method
            var builder = Builders<Burger>.Filter;
            var filter = builder.Eq(b => b.Id, burger.Id);

            await _dbContext.Burger.DeleteOneAsync(filter);
        }

        public async Task<Burger> FindById(string id)
        {
            var burgerCursor = await _dbContext.Burger.FindAsync(b => b.Id.Equals(id));

            var burger = await burgerCursor.FirstOrDefaultAsync();

            return burger;
        }

        public async Task<Burger> FindByName(string name)
        {
            var burgerCursor = await _dbContext.Burger.FindAsync(b => b.Name.Equals(name));
            var burger = await burgerCursor.FirstOrDefaultAsync();
            return burger;
        }

        public async Task<IEnumerable<Burger>> ListAll()
        {
            var documents = await _dbContext.Burger.Find(_ => true).ToListAsync();
            return documents;
        }

        public async Task<Burger> Save(Burger burger)
        {
            await _dbContext.Burger.InsertOneAsync(burger);
            return burger;
        }

        public async Task<Burger> Update(Burger burger)
        {
            //create the filter definition to find the burger document in the collection
            var filterBuilder = Builders<Burger>.Filter;
            var filter = filterBuilder.Eq(b => b.Id, burger.Id);

            // create update definition to supply definition of how to update burger document in the collection
            var udpateBuilder = Builders<Burger>.Update;
            var update = udpateBuilder.Set("name", burger.Name).Set("price", burger.Price);

            await _dbContext.Burger.UpdateOneAsync(filter, update);

            return burger;
        }
    }
}