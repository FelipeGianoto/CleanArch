using CleanArch.Application.Abstractions.Commands;
using CleanArch.Domain.Interfaces;

namespace CleanArch.Application.UseCases.Product.Commands.Create
{
    public class CreateProductHandler(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository
    ) : ICommandHandler<CreateProductCommand, CreateProductOutput>
    {
        public async Task<CreateProductOutput> HandleAsync(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var existsCategory = await categoryRepository.ExistsByIdAsync(command.CategoryId, cancellationToken);
            
            if (!existsCategory)
                throw new InvalidOperationException("Category not found.");

            var product = new Domain.Entities.Product(
                command.Name,
                command.Description,
                command.Price,
                command.Stock,
                command.Image,
                command.CategoryId
            );

            await productRepository.CreateAsync(product, cancellationToken);

            return new CreateProductOutput(product.Id);
        }
    }
}
