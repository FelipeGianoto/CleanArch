using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Application.UseCases.Category.Commands.Delete;
using CleanArch.Application.UseCases.Category.Commands.Update;
using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;

namespace CleanArch.Application.UseCases.Category.Facade
{
    public interface ICategoryFacade
    {
        Task<ListCategoryOutput> ListAsync(ListCategoryQuery query, CancellationToken cancellationToken);
        Task<GetCategoryByIdOutput> GetByIdAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken);
        Task<GetCategoryByIdWithProductsOutput> GetByIdWithProductsAsync(GetCategoryByIdWithProductsQuery query, CancellationToken cancellationToken);
        Task<CreateCategoryOutput> CreateAsync(CreateCategoryCommand command, CancellationToken cancellationToken);
        Task Update(UpdateCategoryCommand command, CancellationToken cancellationToken);
        Task Delete(DeleteCategoryCommand command, CancellationToken cancellationToken);
    }
}
