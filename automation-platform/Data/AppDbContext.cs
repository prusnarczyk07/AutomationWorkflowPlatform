using automation_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace automation_platform.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Workflow> Workflows { get; set; }
    }
}
