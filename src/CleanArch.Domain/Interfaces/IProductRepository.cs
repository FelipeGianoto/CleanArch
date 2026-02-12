using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task CreateAsync(Product category, CancellationToken cancellationToken);
        Task UpdateAsync(Product category, CancellationToken cancellationToken);
        Task DeleteAsync(Product category, CancellationToken cancellationToken);
    }
}
