using DataAccess.DataSource.SqlServer;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Dependencies
{
    public class ServicesDependency
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });
            services.AddScoped<IUserRepository, SqlServerUserRepository>();
            services.AddScoped<IDepartmentRepository, SqlServerDepartmentRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>(); 
        }
    }
}
