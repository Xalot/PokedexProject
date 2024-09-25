using DTOs;
using Moq;
using Moq.Protected;
using PokedexAPI.Services;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace PokedexAPI.Tests
{
    public class PokemonServiceTests
    {
        private readonly Mock<HttpClient> _httpClientMock;
        private readonly PokemonService _pokemonService;

        public PokemonServiceTests()
        {
            _httpClientMock = new Mock<HttpClient>();
            _pokemonService = new PokemonService(_httpClientMock.Object);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnPokemon_WhenIdIsValid()
        {
            // Arrange
            var pokemonJson = "{ \"id\": 1, \"name\": \"bulbasaur\", \"height\": 7, \"weight\": 69, \"sprites\": { \"front_default\": \"someurl\" }, \"types\": [{ \"type\": { \"name\": \"grass\" }}] }";
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(pokemonJson)
            };

            var httpClientHandlerMock = new Mock<HttpMessageHandler>();
            httpClientHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            var httpClient = new HttpClient(httpClientHandlerMock.Object);
            var pokemonService = new PokemonService(httpClient);

            // Act
            var result = await pokemonService.GetPokemonByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("bulbasaur", result.Nombre);
        }
    }
}
