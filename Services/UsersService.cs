using System.Threading.Tasks;
using rest_api_dotnetcore.DTOs;
using rest_api_dotnetcore.Models;
using rest_api_dotnetcore.Repositories;

namespace rest_api_dotnetcore.Services
{
    public interface IUsersService
    {
        Task<string> Login(LoginDTO login);
        Task<User> Register(RegisterDTO user);
    }

    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepo;

        public UsersService(IUsersRepository usersRepo)
        {
            _usersRepo = usersRepo;
        }
        public async Task<string> Login(LoginDTO login)
        {
            return await _usersRepo.Login(login);
        }

        public async Task<User> Register(RegisterDTO user)
        {
            return await _usersRepo.Register(user);
        }
    }
}