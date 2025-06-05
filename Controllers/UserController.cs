using Domain.Models;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Services.Dtos;
using Services.Interfaces;

namespace ApiTienda_GruopMeet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Creamos un objeto de la interfaz
        private readonly IUserService _userService;


        //Generamos el constructor del controlador y le pasamos como parametro el objeto de la interfaz

        public UserController(IUserService userService)
        {
            _userService = userService;

        }


        //Generamos el metódo para mostrar los datos
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        //Generamos el método para guardar un nuevo registro de usuario
        [HttpPost]
         public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUsers), new { id = createdUser.Id }, createdUser);
        }

    }
}
