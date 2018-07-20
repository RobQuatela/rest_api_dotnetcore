using System.Threading.Tasks;
using rest_api_dotnetcore.Models;

namespace rest_api_dotnetcore.Repositories
{
    public interface IRolesRepository
    {
        Task<User> AssignRole(User user, Role role);
        Task<Role> Insert(Role role);
    }

    public class RolesRepository : IRolesRepository
    {
        private readonly DatabaseContext _dbContext;

        public RolesRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<User> AssignRole(User user, Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> Insert(Role role)
        {
            await _dbContext.Role.InsertOneAsync(role);

            return role;
        }
    }
}