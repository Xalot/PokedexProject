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
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly PokemonService _pokemonService;

        public PokemonServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _pokemonService = new PokemonService(_httpClient);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnPokemon_WhenIdIsValid()
        {
            // Arrange
            var pokemonJson = "{ \"id\": 1, \"name\": \"bulbasaur\", \"height\": 7, \"weight\": 69 }";
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(pokemonJson)
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _pokemonService.GetPokemonByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("bulbasaur", result.Nombre);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldReturnNull_WhenPokemonNotFound()
        {
            // Arrange
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _pokemonService.GetPokemonByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPokemonByIdAsync_ShouldThrowException_WhenApiFails()
        {
            // Arrange
            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException());

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => _pokemonService.GetPokemonByIdAsync(1));
        }
    }
}
