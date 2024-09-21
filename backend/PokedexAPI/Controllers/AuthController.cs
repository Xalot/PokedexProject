using DTOs;
using Microsoft.AspNetCore.Mvc;
using PokedexAPI.Services;
using System.Threading.Tasks;

namespace PokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Registro de usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            await _userService.AddUserAsync(user);
            return Ok(new { message = "Usuario registrado exitosamente" });
        }

        // Iniciar sesión (Esto después se modificará para manejar JWT)
        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.GetUserByUsernameAsync(username);

            if (user == null || user.PasswordHash != password) // Simplificado
                return Unauthorized(new { message = "Credenciales incorrectas" });

            // Aquí se generará y devolverá el token JWT en el futuro
            return Ok(new { message = "Inicio de sesión exitoso" });
        }

    }
}
