using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.Abstractions.Queries;

namespace CleanArch.Application.UseCases.Category.Query.GetById
{
    public sealed class GetCategoryByIdHandler(
        ICategoryReadRepository categoryReadRepository
    ) 
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdOutput>
    {
        public async Task<GetCategoryByIdOutput> HandleAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var categoryOutput = await categoryReadRepository.GetByIdAsync(query.Id, cancellationToken) 
                ?? throw new KeyNotFoundException($"Category with id {query.Id} not found.");
            return categoryOutput;
        }
    }
}
