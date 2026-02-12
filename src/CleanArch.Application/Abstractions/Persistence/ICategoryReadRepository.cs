using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;

namespace CleanArch.Application.Abstractions.Persistence
{
    public interface ICategoryReadRepository
    {
        Task<GetCategoryByIdOutput?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<CategoryByIdWithProductsOutput?> GetByIdWithProductsAsync(int id, int Page, int PageSize, CancellationToken cancellationToken);
        Task<int> CountProductsByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryListOutput>> ListAsync(CancellationToken cancellationToken);
    }
}
