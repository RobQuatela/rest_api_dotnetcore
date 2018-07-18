using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rest_api_dotnetcore.Models;
using rest_api_dotnetcore.Services;

namespace rest_api_dotnetcore.Controllers
{
    [Route("api/[controller]")]
    public class BurgersController : Controller
    {
        private readonly IBurgersService _burgersService;

        public BurgersController(IBurgersService burgersService)
        {
            _burgersService = burgersService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAllBurgers()
        {
            var burgers = await _burgersService.ListAll();

            if(burgers == null)
                return NotFound();
            
            return Ok(burgers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBurgerById(string id)
        {
            var burger = await _burgersService.FindById(id);

            if(burger == null)
                return StatusCode(400);
            
            return Ok(burger);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetBurgerByName(string name)
        {
            var burger = await _burgersService.FindByName(name);

            if(burger == null)
                return StatusCode(400);
            
            return Ok(burger);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBurger([FromBody] Burger burger)
        {
            var burgerSaved = await _burgersService.Save(burger);

            if(burger == null)
                return StatusCode(400);
            
            return Ok(burgerSaved);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBurger([FromBody] Burger burger)
        {
            var burgerUpdated = await _burgersService.Update(burger);

            if(burgerUpdated == null)
                return StatusCode(400);
            
            return Ok(burgerUpdated);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveBurger(string id)
        {
            try {
                await _burgersService.Delete(id);
            } catch {
                return StatusCode(400);
            }

            return StatusCode(201);
        }
    }
}