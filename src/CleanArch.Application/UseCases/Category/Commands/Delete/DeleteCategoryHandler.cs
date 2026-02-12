using CleanArch.Application.Abstractions.Commands;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Category.Commands.Delete
{
    public class DeleteCategoryHandler(
        ICategoryRepository categoryRepository
    ) : ICommandHandler<DeleteCategoryCommand>
    {
        async Task ICommandHandler<DeleteCategoryCommand>.HandleAsync(DeleteCategoryCommand query, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByWhereAsync(category => category.Id == query.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Category with id {query.Id} not found.");
            
            await categoryRepository.DeleteAsync(category, cancellationToken);
        }
    }
}
