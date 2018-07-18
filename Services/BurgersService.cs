using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using rest_api_dotnetcore.Models;
using rest_api_dotnetcore.Repositories;

namespace rest_api_dotnetcore.Services
{
    public interface IBurgersService
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

    public class BurgersService : IBurgersService
    {
        private readonly IBurgersRepo _burgersRepo;

        public BurgersService(IBurgersRepo burgersRepo)
        {
            _burgersRepo = burgersRepo;
        }
        public async Task Delete(string id)
        {
            await _burgersRepo.Delete(id);
        }

        public async Task<Burger> FindById(string id)
        {
            return await _burgersRepo.FindById(id);
        }

        public async Task<Burger> FindByName(string name)
        {
            return await _burgersRepo.FindByName(name);
        }

        public async Task<IEnumerable<Burger>> ListAll()
        {
            return await _burgersRepo.ListAll();
        }

        public async Task<Burger> Save(Burger burger)
        {
            return await _burgersRepo.Save(burger);
        }

        public async Task<Burger> Update(Burger burger)
        {
            return await _burgersRepo.Update(burger);
        }
    }
}