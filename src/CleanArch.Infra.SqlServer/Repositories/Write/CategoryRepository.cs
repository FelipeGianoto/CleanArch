using CleanArch.Domain.Entities;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArch.Infra.SqlServer.Repositories.Write
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<Category> _dbSet = context.Set<Category>();

        public async Task CreateAsync(Category category, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(category, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Category category, CancellationToken cancellationToken)
        {
            _dbSet.Remove(category);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Category?> GetByWhereAsync(
            Expression<Func<Category, bool>> predicate,
            CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _dbSet
                .AsNoTracking()
                .AnyAsync(category => category.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken)
        {
            _dbSet.Update(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
