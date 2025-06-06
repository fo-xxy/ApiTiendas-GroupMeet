using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Interfaces;
using Services.Services;

namespace ApiTienda_GruopMeet.Controllers
{
    [Route("api/login/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //Creamos un objeto de la interfaz
        private readonly IAuthService _authService;

        //Generamos el constructor del controlador y le pasamos como parametro el objeto de la interfaz
        public LoginController(IAuthService authService)
        {
            _authService = authService;
        }

        //Generamos el metódo para mostrar los datos
        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _authService.AuthenticateAsync(dto);

            if (token == null)
                return Unauthorized("Contraseña incorrecta.");

            return Ok(new { token });
        }

    }
}
