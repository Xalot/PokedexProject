using DTOs;
using System.Threading.Tasks;

namespace PokedexAPI.Services
{
    public interface IPokemonService
    {
        // Obtener Pokémon por su ID
        Task<Pokemon> GetPokemonByIdAsync(int id);

        // Obtener Pokémon por su nombre
        Task<Pokemon> GetPokemonByNameAsync(string name);
    }
}

