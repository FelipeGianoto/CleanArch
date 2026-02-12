using CleanArch.Application.Abstractions.Persistence;
using CleanArch.Application.Abstractions.Queries;

namespace CleanArch.Application.UseCases.Product.Query.GetById
{
    public class GetProductByIdHandler(
        IProductReadRepository productReadRepository
    ) : IQueryHandler<GetProductByIdQuery, GetProductByIdOutput>
    {
        public async Task<GetProductByIdOutput> HandleAsync(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var productOutput = await productReadRepository
                .GetByIdWithCategoryAsync(query.Id, cancellationToken)
                 ?? throw new KeyNotFoundException($"Product with id {query.Id} not found.");
            
            return productOutput;
        }
    }
}
