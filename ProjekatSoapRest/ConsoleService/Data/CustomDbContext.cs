using System.Data.Entity;
using ConsoleService.Model;

namespace ConsoleService.Data
{
    public class CustomDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public CustomDbContext() : base("name=MyPostgresConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
