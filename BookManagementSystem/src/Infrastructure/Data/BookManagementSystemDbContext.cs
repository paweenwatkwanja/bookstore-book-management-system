using Microsoft.EntityFrameworkCore;

namespace Data;

public class BookManagementSystemDbContext : DbContext
{
    public DbSet<HealthcheckEntity> Healthchecks {get; set;}

    public BookManagementSystemDbContext(DbContextOptions options) : base(options)
    {
    }
}