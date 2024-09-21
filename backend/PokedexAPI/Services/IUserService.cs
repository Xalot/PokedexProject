using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexAPI.Services
{
    public interface IUserService
    {
        // Obtener todos los usuarios
        Task<IEnumerable<User>> GetAllUsersAsync();

        // Obtener un usuario por su ID
        Task<User> GetUserByIdAsync(int id);

        // Crear un nuevo usuario
        Task AddUserAsync(User user);

        // Actualizar un usuario existente
        Task UpdateUserAsync(User user);

        // Eliminar un usuario
        Task DeleteUserAsync(int id);

        // Obtener un usuario por su nombre de usuario
        Task<User> GetUserByUsernameAsync(string username);
    }
}
