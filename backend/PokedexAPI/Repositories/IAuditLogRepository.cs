using System.Threading.Tasks;

namespace PokedexAPI.Repositories
{
    public interface IAuditLogRepository
    {
        Task LogActionAsync(int userId, string action);
    }
}
