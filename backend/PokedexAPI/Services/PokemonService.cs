using DTOs;
using Newtonsoft.Json;
using log4net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PokedexAPI.Services
{
    public class PokemonService : IPokemonService
    {
        private readonly HttpClient _httpClient;
        private readonly string _pokeApiUrl = "https://pokeapi.co/api/v2/";
        private readonly ILog _logger = LogManager.GetLogger(typeof(PokemonService));

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Obtener Pokémon por su ID
        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            _logger.Info($"Intentando obtener Pokémon por ID: {id}");
            return await GetPokemonDataAsync($"pokemon/{id}");
        }

        // Obtener Pokémon por su nombre
        public async Task<Pokemon> GetPokemonByNameAsync(string name)
        {
            _logger.Info($"Intentando obtener Pokémon por nombre: {name}");
            return await GetPokemonDataAsync($"pokemon/{name}");
        }

        // Método privado para obtener los datos del Pokémon por ID o nombre
        private async Task<Pokemon> GetPokemonDataAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_pokeApiUrl}{endpoint}");

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    dynamic pokemonData = JsonConvert.DeserializeObject(jsonContent);

                    if (pokemonData == null) return null;

                    var pokemon = new Pokemon
                    {
                        Id = pokemonData.id ?? 0,
                        Nombre = pokemonData.name ?? "Desconocido",
                        Imagenfrontal = pokemonData.sprites?.front_default ?? string.Empty,
                        Tipos = new List<string>(),
                        Altura = pokemonData.height ?? 0,
                        Peso = pokemonData.weight ?? 0
                    };

                    if (pokemonData.types != null)
                    {
                        foreach (var typeInfo in pokemonData.types)
                        {
                            pokemon.Tipos.Add((string)typeInfo?.type?.name ?? "Desconocido");
                        }
                    }

                    var speciesUrl = pokemonData.species?.url?.ToString();
                    if (!string.IsNullOrEmpty(speciesUrl))
                    {
                        var speciesResponse = await _httpClient.GetAsync(speciesUrl);
                        if (speciesResponse.IsSuccessStatusCode)
                        {
                            var speciesJson = await speciesResponse.Content.ReadAsStringAsync();
                            dynamic speciesData = JsonConvert.DeserializeObject(speciesJson);

                            if (speciesData?.flavor_text_entries != null)
                            {
                                foreach (var flavorTextEntry in speciesData.flavor_text_entries)
                                {
                                    if (flavorTextEntry.language.name == "es")
                                    {
                                        pokemon.Descripcion = flavorTextEntry.flavor_text ?? "Sin descripción";
                                        break;
                                    }
                                }
                            }

                            var evolutionUrl = speciesData?.evolution_chain?.url?.ToString();
                            if (!string.IsNullOrEmpty(evolutionUrl))
                            {
                                var evolutionResponse = await _httpClient.GetAsync(evolutionUrl);
                                if (evolutionResponse.IsSuccessStatusCode)
                                {
                                    var evolutionJson = await evolutionResponse.Content.ReadAsStringAsync();
                                    dynamic evolutionData = JsonConvert.DeserializeObject(evolutionJson);

                                    pokemon.Cadenaevolucion = new Cadenadeevolucion
                                    {
                                        PokemonBebe = evolutionData.chain?.species?.name ?? "Desconocido",
                                        PrimeraEvolucion = evolutionData.chain?.evolves_to?.Count > 0 ? evolutionData.chain.evolves_to[0].species.name : null,
                                        SiguienteEvolucion = evolutionData.chain?.evolves_to?.Count > 0 && evolutionData.chain.evolves_to[0].evolves_to?.Count > 0 ?
                                                            evolutionData.chain.evolves_to[0].evolves_to[0].species.name : null
                                    };
                                }
                            }
                        }
                    }

                    _logger.Info($"Pokémon {pokemon.Nombre} obtenido exitosamente");
                    return pokemon;
                }
                else
                {
                    _logger.Warn($"Error al obtener datos del Pokémon desde PokeAPI. Estado: {response.StatusCode}");
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al obtener datos del Pokémon: {ex.Message}", ex);
                throw;
            }
        }

    }
}
