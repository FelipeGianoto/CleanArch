using CleanArch.Application.Abstractions.Commands;
using CleanArch.Application.Abstractions.Queries;
using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Application.UseCases.Category.Commands.Delete;
using CleanArch.Application.UseCases.Category.Commands.Update;
using CleanArch.Application.UseCases.Category.Facade;
using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;
using CleanArch.Application.UseCases.Product.Commands.Create;
using CleanArch.Application.UseCases.Product.Commands.Delete;
using CleanArch.Application.UseCases.Product.Commands.Update;
using CleanArch.Application.UseCases.Product.Facade;
using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC.Extensions
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            #region UseCases Category
            services
                .AddScoped<IQueryHandler<ListCategoryQuery, ListCategoryOutput>, ListCategoryHandler>()
                .AddScoped<IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdOutput>, GetCategoryByIdHandler>()
                .AddScoped<IQueryHandler<GetCategoryByIdWithProductsQuery, GetCategoryByIdWithProductsOutput>, GetCategoryByIdWithProductsHandler>()
                .AddScoped<ICommandHandler<CreateCategoryCommand, CreateCategoryOutput>, CreateCategoryHandler>()
                .AddScoped<ICommandHandler<DeleteCategoryCommand>, DeleteCategoryHandler>()
                .AddScoped<ICommandHandler<UpdateCategoryCommand>, UpdateCategoryHandler>();
            #endregion

            #region UseCases Product
            services
                .AddScoped<IQueryHandler<ListProductQuery, ListProductOutput>, ListProductHandler>()
                .AddScoped<IQueryHandler<GetProductByIdQuery, GetProductByIdOutput>, GetProductByIdHandler>()
                .AddScoped<ICommandHandler<CreateProductCommand, CreateProductOutput>, CreateProductHandler>()
                .AddScoped<ICommandHandler<DeleteProductCommand>, DeleteProductHandler>()
                .AddScoped<ICommandHandler<UpdateProductCommand>, UpdateProductHandler>();
            #endregion

            #region Facades
            services
                .AddScoped<ICategoryFacade, CategoryFacade>()
                .AddScoped<IProductFacade, ProductFacade>();
            #endregion

            return services;
        }
    }
}
