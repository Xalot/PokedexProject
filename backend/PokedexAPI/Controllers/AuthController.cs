using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PokedexAPI.Services;
using log4net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AuthController));
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [Authorize]
        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                _logger.Info("Intentando obtener todos los usuarios");
                var users = await _userService.GetAllUsersAsync();
                _logger.Info("Usuarios obtenidos correctamente");
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                _logger.Error("Error al obtener usuarios", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        // Registro de usuario
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                _logger.Info($"Intentando registrar usuario: {user.Username}");
                await _userService.AddUserAsync(user);
                _logger.Info($"Usuario {user.Username} registrado correctamente");
                return Ok(new { message = "Usuario registrado exitosamente" });
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al registrar usuario {user.Username}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                _logger.Info($"Intentando iniciar sesión para el usuario: {username}");
                var user = await _userService.GetUserByUsernameAsync(username);

                if (user == null || user.PasswordHash != password)
                {
                    _logger.Warn($"Credenciales incorrectas para el usuario: {username}");
                    return Unauthorized(new { message = "Credenciales incorrectas" });
                }

                // Generar token JWT
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key is missing from configuration"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                _logger.Info($"Usuario {username} ha iniciado sesión exitosamente");
                return Ok(new { token = tokenString });
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al iniciar sesión para el usuario {username}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        // Eliminar usuario
        [Authorize]
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                _logger.Info($"Intentando eliminar usuario con ID: {id}");
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    _logger.Warn($"Usuario con ID {id} no encontrado");
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                await _userService.DeleteUserAsync(id);
                _logger.Info($"Usuario con ID {id} eliminado correctamente");
                return Ok(new { message = "Usuario eliminado exitosamente" });
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al eliminar usuario con ID {id}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [Authorize]
        [HttpPut("updateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
        {
            try
            {
                _logger.Info($"Intentando actualizar usuario con ID: {id}");
                var existingUser = await _userService.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    _logger.Warn($"Usuario con ID {id} no encontrado");
                    return NotFound(new { message = "Usuario no encontrado" });
                }

                existingUser.Username = updatedUser.Username;
                existingUser.PasswordHash = updatedUser.PasswordHash;
                existingUser.Email = updatedUser.Email;

                await _userService.UpdateUserAsync(existingUser);
                _logger.Info($"Usuario con ID {id} actualizado correctamente");
                return Ok(new { message = "Usuario actualizado exitosamente" });
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al actualizar usuario con ID {id}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
