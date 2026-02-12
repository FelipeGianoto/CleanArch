using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.Abstractions.Queries;

namespace CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts
{
    public class GetCategoryByIdWithProductsHandler(
        ICategoryReadRepository categoryReadRepository
    ) 
        : IQueryHandler<GetCategoryByIdWithProductsQuery, GetCategoryByIdWithProductsOutput>
    {
        public async Task<GetCategoryByIdWithProductsOutput> HandleAsync(GetCategoryByIdWithProductsQuery query, CancellationToken cancellationToken)
        {
            var categoryOutput = await categoryReadRepository.GetByIdWithProductsAsync(
                query.Id,
                query.Page,
                query.PageSize,
                cancellationToken
            ) ?? throw new KeyNotFoundException($"Category with id {query.Id} not found.");

            var count = await categoryReadRepository.CountProductsByIdAsync(query.Id, cancellationToken);

            return new GetCategoryByIdWithProductsOutput(query.Page, query.PageSize, count, categoryOutput);
        }
    }
}
