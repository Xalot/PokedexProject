using Microsoft.AspNetCore.Mvc;
using PokedexAPI.Services;
using log4net;
using System.Threading.Tasks;
using PokedexAPI.Repositories;

namespace PokedexAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(PokemonController));
        private readonly IPokemonService _pokemonService;
        private readonly IAuditLogRepository _auditLogRepository;  // Servicio de auditoría

        public PokemonController(IPokemonService pokemonService, IAuditLogRepository auditLogRepository)
        {
            _pokemonService = pokemonService;
            _auditLogRepository = auditLogRepository;  // Inyectamos el servicio de auditoría
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            try
            {
                _logger.Info($"Intentando obtener Pokémon por ID: {id}");
                var pokemon = await _pokemonService.GetPokemonByIdAsync(id);

                if (pokemon == null)
                {
                    _logger.Warn($"Pokémon con ID {id} no encontrado");
                    return NotFound(new { message = "Pokémon no encontrado" });
                }

                // Registrar en los logs de auditoría
                await _auditLogRepository.LogActionAsync(id, "Consulta de Pokémon por ID");

                _logger.Info($"Pokémon {pokemon.Nombre} obtenido correctamente");
                return Ok(pokemon);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al obtener Pokémon con ID {id}: {ex.Message}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }

        [HttpGet("nombre/{name}")]
        public async Task<IActionResult> GetPokemonByName(string name)
        {
            try
            {
                _logger.Info($"Intentando obtener Pokémon por nombre: {name}");
                var pokemon = await _pokemonService.GetPokemonByNameAsync(name);

                if (pokemon == null)
                {
                    _logger.Warn($"Pokémon con nombre {name} no encontrado");
                    return NotFound(new { message = "Pokémon no encontrado" });
                }

                // Registrar en los logs de auditoría
                await _auditLogRepository.LogActionAsync(pokemon.Id, $"Consulta de Pokémon por nombre: {name}");

                _logger.Info($"Pokémon {pokemon.Nombre} obtenido correctamente");
                return Ok(pokemon);
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al obtener Pokémon con nombre {name}: {ex.Message}", ex);
                return StatusCode(500, new { message = "Error interno del servidor" });
            }
        }
    }
}
