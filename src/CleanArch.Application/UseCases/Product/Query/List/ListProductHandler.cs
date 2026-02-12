using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.Abstractions.Queries;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Product.Query.List
{
    public class ListProductHandler(
        IProductReadRepository productReadRepository
    ) : IQueryHandler<ListProductQuery, ListProductOutput>
    {
        public async Task<ListProductOutput> HandleAsync(ListProductQuery query, CancellationToken cancellationToken)
        {
            var products = await productReadRepository.ListAsync(cancellationToken);
            return new ListProductOutput(products);
        }
    }
}
