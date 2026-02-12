using CleanArch.Domain.Entities;
using System.Linq.Expressions;

namespace CleanArch.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByWhereAsync(Expression<Func<Category, bool>> predicate,
            CancellationToken cancellationToken);

        Task<bool> ExistsByIdAsync(int id,
            CancellationToken cancellationToken);

        Task CreateAsync(Category category, CancellationToken cancellationToken);
        Task UpdateAsync(Category category, CancellationToken cancellationToken);
        Task DeleteAsync(Category category, CancellationToken cancellationToken);
    }
}
