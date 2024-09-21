using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PokedexAPI.Repositories
{
    public interface IUserRepository
    {
        // Obtener todos los usuarios
        Task<IEnumerable<User>> GetAllAsync();

        // Obtener un usuario por su ID
        Task<User> GetByIdAsync(int id);

        // Agregar un nuevo usuario
        Task AddAsync(User user);

        // Actualizar un usuario existente
        Task UpdateAsync(User user);

        // Eliminar un usuario por su ID
        Task DeleteAsync(int id);

        // Obtener un usuario por su nombre de usuario
        Task<User> GetByUsernameAsync(string username);
    }
}
