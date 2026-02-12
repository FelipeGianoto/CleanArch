using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.Abstractions.Queries;

namespace CleanArch.Application.UseCases.Category.Query.List
{
    public sealed class ListCategoryHandler(
        ICategoryReadRepository categoryReadRepository
    ) 
        : IQueryHandler<ListCategoryQuery, ListCategoryOutput>
    {
        public async Task<ListCategoryOutput> HandleAsync(
            ListCategoryQuery query,
            CancellationToken cancellationToken)
        {
            var categories = await categoryReadRepository.ListAsync(cancellationToken);
            return new ListCategoryOutput(categories);
        }
    }
}
