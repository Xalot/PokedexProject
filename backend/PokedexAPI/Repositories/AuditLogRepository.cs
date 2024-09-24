using DTOs;
using log4net;
using System.Threading.Tasks;

namespace PokedexAPI.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AuditLogRepository));
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(int userId, string action)
        {
            try
            {
                _logger.Info($"Registrando acción: {action} para el usuario con ID: {userId}");
                var auditLog = new AuditLog
                {
                    UserId = userId,
                    Accion = action,
                    FechaHora = DateTime.Now
                };

                await _context.AuditLogs.AddAsync(auditLog);
                await _context.SaveChangesAsync();
                _logger.Info($"Acción registrada correctamente para el usuario con ID: {userId}");
            }
            catch (System.Exception ex)
            {
                _logger.Error($"Error al registrar acción {action} para el usuario con ID {userId}.", ex);
                throw;
            }
        }
    }
}
