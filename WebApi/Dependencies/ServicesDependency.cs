using DataAccess.DataSource.SqlServer;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using WebApi.Interfaces;
using WebApi.Services;

namespace WebApi.Dependencies
{
    public static class ServicesDependency
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TestDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
            });
            services.AddScoped<IUserRepository, SqlServerUserRepository>();
            services.AddScoped<IDepartmentRepository, SqlServerDepartmentRepository>();
            services.AddScoped<IJobTitleRepository, SqlServerJobTitleRepository>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IJobTitleService, JobTitleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddAutoMapper(typeof(Program));
        }
    }
}
