using CleanArch.Application.Abstractions.Commands;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Product.Commands.Delete
{
    public class DeleteProductHandler(
        IProductRepository productRepository
    ) 
        : ICommandHandler<DeleteProductCommand>
    {
        public async Task HandleAsync(DeleteProductCommand query, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(query.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Product with id {query.Id} not found.");

            await productRepository.DeleteAsync(product, cancellationToken);
        }
    }
}
