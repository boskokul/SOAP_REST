using System.Data.Entity;

namespace ProjekatSoapRest.Data
{
    public class CustomDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public CustomDbContext() : base("name=MyPostgresConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Company>().ToTable("Companies");
        }
    }
}