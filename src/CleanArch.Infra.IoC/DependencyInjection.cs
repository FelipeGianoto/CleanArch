using CleanArch.Infra.IoC.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            bool isTestEnviroment)
        {
            if (!isTestEnviroment)
            {
                services.AddSqlServerInfra(configuration);
            }

            services
                .AddApplication()
                .AddRepositories();

            return services;
        }
    }
}
