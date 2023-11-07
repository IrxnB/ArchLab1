using ArchLab1Lib.Model;
using Microsoft.EntityFrameworkCore;

namespace Server.Model
{
    internal class ArchDbContext : DbContext
    {
        public ArchDbContext(DbContextOptions options) : base(options) { }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
