using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC.Extensions
{
    public static class SqlServerDependencyInjection
    {
        public static IServiceCollection AddSqlServerInfra(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("SqlServerConnection"),
                    sql =>
                        sql.MigrationsAssembly(
                            typeof(AppDbContext).Assembly.FullName)));

            return services;
        }
    }
}
