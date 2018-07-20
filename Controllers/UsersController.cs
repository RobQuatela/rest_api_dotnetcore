using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rest_api_dotnetcore.DTOs;
using rest_api_dotnetcore.Models;
using rest_api_dotnetcore.Services;

namespace rest_api_dotnetcore.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO user)
        {
            var userToReturn = await _usersService.Register(user);

            if(userToReturn == null)
                return StatusCode(400);
            
            return Ok(userToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            var token = await _usersService.Login(login);

            if(token == null)
                return NotFound();
            
            return Ok(token);
        }
    }
}