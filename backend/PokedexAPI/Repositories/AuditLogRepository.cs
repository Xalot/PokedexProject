using DTOs;
using System.Threading.Tasks;

namespace PokedexAPI.Repositories
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(int userId, string action)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Accion = action,
                FechaHora = DateTime.Now
            };

            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}
