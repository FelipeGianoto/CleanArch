using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.SqlServer.Repositories.Read;
using CleanArch.Infra.SqlServer.Repositories.Write;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC.Extensions
{
    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            #region Write Repositories
            services
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            #region Read Repositories
            services
                .AddScoped<IProductReadRepository, ProductReadRepository>()
                .AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            #endregion

            return services;
        }
    }
}
