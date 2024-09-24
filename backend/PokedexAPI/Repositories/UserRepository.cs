using DTOs;
using Microsoft.EntityFrameworkCore;
using log4net;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserRepository));
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener todos los usuarios
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            try
            {
                _logger.Info("Intentando obtener todos los usuarios.");
                var users = await _context.Users.ToListAsync();
                _logger.Info("Usuarios obtenidos correctamente.");
                return users;
            }
            catch (System.Exception ex)
            {
                _logger.Error("Error al obtener los usuarios.", ex);
                throw;
            }
        }

        // Obtener un usuario por su ID
        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                _logger.Info($"Intentando obtener el usuario con ID {id}.");
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    _logger.Warn($"Usuario con ID {id} no encontrado.");
                }
                else
                {
                    _logger.Info($"Usuario con ID {id} obtenido correctamente.");
                }
                return user;
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al obtener el usuario con ID {id}.", ex);
                throw;
            }
        }

        // Agregar un nuevo usuario
        public async Task AddAsync(User user)
        {
            try
            {
                _logger.Info($"Intentando agregar un nuevo usuario: {user.Username}");
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                _logger.Info($"Usuario {user.Username} agregado correctamente.");
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al agregar el usuario {user.Username}.", ex);
                throw;
            }
        }

        // Actualizar un usuario existente
        public async Task UpdateAsync(User user)
        {
            try
            {
                _logger.Info($"Intentando actualizar el usuario con ID {user.Id}.");
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                _logger.Info($"Usuario con ID {user.Id} actualizado correctamente.");
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al actualizar el usuario con ID {user.Id}.", ex);
                throw;
            }
        }

        // Eliminar un usuario por su ID
        public async Task DeleteAsync(int id)
        {
            try
            {
                _logger.Info($"Intentando eliminar el usuario con ID {id}.");
                var user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                    _logger.Info($"Usuario con ID {id} eliminado correctamente.");
                }
                else
                {
                    _logger.Warn($"Usuario con ID {id} no encontrado.");
                }
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al eliminar el usuario con ID {id}.", ex);
                throw;
            }
        }

        // Obtener un usuario por su nombre de usuario
        public async Task<User> GetByUsernameAsync(string username)
        {
            try
            {
                _logger.Info($"Intentando obtener el usuario con nombre de usuario: {username}");
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    _logger.Warn($"Usuario con nombre {username} no encontrado.");
                }
                else
                {
                    _logger.Info($"Usuario {username} obtenido correctamente.");
                }
                return user;
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al obtener el usuario con nombre {username}.", ex);
                throw;
            }
        }
    }
}
