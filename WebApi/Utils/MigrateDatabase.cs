using DataAccess.DataSource.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Utils {
    
    public static class MigrateDatabaseUtil{
        public static void MigrateDatabase(this IServiceCollection services){
            var dbContext = services.BuildServiceProvider().GetRequiredService<TestDbContext>();
            dbContext.Database.Migrate();
        }
    }
}