using CleanArch.Application.Abstractions.Commands;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Category.Commands.Create
{
    public sealed class CreateCategoryHandler(
        ICategoryRepository repository
    )
        : ICommandHandler<CreateCategoryCommand, CreateCategoryOutput>
    {
        private readonly ICategoryRepository _repository = repository;

        public async Task<CreateCategoryOutput> HandleAsync(
            CreateCategoryCommand command,
            CancellationToken cancellationToken)
        {
            var category = new Domain.Entities.Category(command.Name);

            await _repository.CreateAsync(category, cancellationToken);

            return new CreateCategoryOutput(category.Id);
        }
    }
}
