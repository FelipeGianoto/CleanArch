using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;

namespace CleanArch.Application.Abstractions.Persistence
{
    public interface IProductReadRepository
    {
        Task<GetProductByIdOutput?> GetByIdWithCategoryAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<ProductListOutput>> ListAsync(CancellationToken cancellationToken);
    }
}
