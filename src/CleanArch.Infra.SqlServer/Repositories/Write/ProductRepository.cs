using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infra.SqlServer.Repositories.Write
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<Product> _dbSet = context.Set<Product>();

        public async Task CreateAsync(Product product, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken)
        {
            _dbSet.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken)
        {
            _dbSet.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
        }
    }
}
