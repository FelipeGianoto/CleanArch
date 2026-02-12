using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;
using CleanArch.Domain.Entities;
using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.SqlServer.Repositories.Read
{
    public class CategoryReadRepository(AppDbContext context) : ICategoryReadRepository
    {
        private readonly DbSet<Category> _dbSet = context.Set<Category>();

        public async Task<GetCategoryByIdOutput?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(category => category.Id == id)
                .Select(category => new GetCategoryByIdOutput(
                    new CategoryByIdOutput(
                        category.Id,
                        category.Name,
                        category.CreatedAt,
                        category.UpdatedAt
                    )
                ))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<CategoryByIdWithProductsOutput?> GetByIdWithProductsAsync(int id, int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(category => category.Id == id)
                .Select(c => new CategoryByIdWithProductsOutput(
                    c.Id,
                    c.Name,
                    c.Products
                        .OrderBy(p => p.Id)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .Select(p => new ProductCategoryByIdWithProductsOutput(
                            p.Id,
                            p.Name,
                            p.Description,
                            p.Price,
                            p.Stock
                        ))
                ))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CountProductsByIdAsync(
            int id,
            CancellationToken cancellationToken)
        {
            var category = await _dbSet
                .AsNoTracking()
                .Where(category => category.Id == id)
                .Select(c => new
                {
                    ProductCount = c.Products.Count
                })
                .FirstOrDefaultAsync(cancellationToken);

            return category?.ProductCount ?? 0;
        }

        public async Task<IEnumerable<CategoryListOutput>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(p => new CategoryListOutput(
                    p.Id,
                    p.Name
                ))
                .ToListAsync(cancellationToken) ?? [];
        }
    }
}
