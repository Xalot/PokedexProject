using DTOs;
using PokedexAPI.Repositories;
using log4net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokedexAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(UserService));

        private readonly IUserRepository _userRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public UserService(IUserRepository userRepository, IAuditLogRepository auditLogRepository)
        {
            _userRepository = userRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                _logger.Info("Obteniendo todos los usuarios");
                var users = await _userRepository.GetAllAsync();
                _logger.Info("Usuarios obtenidos correctamente");
                return users;
            }
            catch (Exception ex)
            {
                _logger.Error("Error al obtener usuarios", ex);
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                _logger.Info($"Obteniendo usuario con ID: {id}");
                var user = await _userRepository.GetByIdAsync(id);
                if (user != null)
                {
                    _logger.Info($"Usuario encontrado: {user.Username}");
                }
                else
                {
                    _logger.Warn($"Usuario con ID {id} no encontrado");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al obtener usuario con ID {id}", ex);
                throw;
            }
        }

        public async Task AddUserAsync(User user)
        {
            try
            {
                _logger.Info($"Intentando agregar usuario: {user.Username}");
                await _userRepository.AddAsync(user);
                await _auditLogRepository.LogActionAsync(user.Id, "Registro de nuevo usuario");
                _logger.Info($"Usuario {user.Username} agregado correctamente");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al agregar usuario {user.Username}", ex);
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                _logger.Info($"Intentando actualizar usuario: {user.Username}");
                await _userRepository.UpdateAsync(user);
                await _auditLogRepository.LogActionAsync(user.Id, "Actualización de usuario");
                _logger.Info($"Usuario {user.Username} actualizado correctamente");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al actualizar usuario {user.Username}", ex);
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                _logger.Info($"Intentando eliminar usuario con ID: {id}");
                await _userRepository.DeleteAsync(id);
                await _auditLogRepository.LogActionAsync(id, "Eliminación de usuario");
                _logger.Info($"Usuario con ID {id} eliminado correctamente");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al eliminar usuario con ID {id}", ex);
                throw;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                _logger.Info($"Obteniendo usuario con nombre de usuario: {username}");
                var user = await _userRepository.GetByUsernameAsync(username);
                if (user != null)
                {
                    _logger.Info($"Usuario encontrado: {user.Username}");
                }
                else
                {
                    _logger.Warn($"Usuario con nombre de usuario {username} no encontrado");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.Error($"Error al obtener usuario con nombre de usuario {username}", ex);
                throw;
            }
        }
    }
}
