using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DataSource.SqlServer
{
    public class TestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                var builder = new ConfigurationBuilder()
                    .AddJsonFile(appSettingsPath);

                IConfigurationRoot configuration = builder.Build();
                string connectionString = configuration.GetConnectionString("SqlServerConnection");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
