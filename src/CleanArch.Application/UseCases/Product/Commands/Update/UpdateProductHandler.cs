using CleanArch.Application.Abstractions.Commands;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Product.Commands.Update
{
    public class UpdateProductHandler(
        IProductRepository productRepository
    ) 
        : ICommandHandler<UpdateProductCommand>
    {
        public async Task HandleAsync(UpdateProductCommand query, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(query.Id, cancellationToken)
                ?? throw new KeyNotFoundException($"Category with id {query.Id} not found.");

            product.Update(
                query.Name,
                query.Description,
                query.Price,
                query.Stock,
                query.Image,
                query.CategoryId
            );

            await productRepository.UpdateAsync(product, cancellationToken);
        }
    }
}
