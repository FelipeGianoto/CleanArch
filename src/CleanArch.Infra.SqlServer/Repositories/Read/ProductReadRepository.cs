using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;
using CleanArch.Domain.Entities;
using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.SqlServer.Repositories.Read
{
    public class ProductReadRepository(AppDbContext context) : IProductReadRepository
    {
        private readonly DbSet<Product> _dbSet = context.Set<Product>();

        public async Task<GetProductByIdOutput?> GetByIdWithCategoryAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new GetProductByIdOutput(
                    new ProductByIdOutput(
                        p.Id,
                        p.Name,
                        p.Description,
                        p.Price,
                        p.Stock,
                        p.Image,
                        p.Category == null
                            ? null
                            : new CategoryProductByIdOutput(
                                p.Category.Id,
                                p.Category.Name
                            )
                    )
                ))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<ProductListOutput>> ListAsync(CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(p => new ProductListOutput(
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.Stock,
                    p.Image,
                    p.CategoryId
                ))
                .ToListAsync(cancellationToken) ?? [];
        }
    }
}
