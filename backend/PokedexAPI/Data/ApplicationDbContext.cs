using Microsoft.EntityFrameworkCore;
using DTOs; 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets para las tablas de la base de datos
    public DbSet<User> Users { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }
}
