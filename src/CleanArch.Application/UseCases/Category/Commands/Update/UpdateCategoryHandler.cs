using CleanArch.Application.Abstractions.Commands;
using CleanArch.Application.Abstractions.Queries;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Category.Commands.Update
{
    public sealed class UpdateCategoryHandler(
        ICategoryRepository categoryRepository
    ) : ICommandHandler<UpdateCategoryCommand>
    {
        public async Task HandleAsync(UpdateCategoryCommand query, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByWhereAsync(category => category.Id == query.Id, cancellationToken) 
                ?? throw new KeyNotFoundException($"Category with id {query.Id} not found.");

            category.Update(query.Name);

            await categoryRepository.UpdateAsync(category, cancellationToken);
        }
    }
}
