using CleanArch.Application.UseCases.Product.Commands.Create;
using CleanArch.Application.UseCases.Product.Commands.Delete;
using CleanArch.Application.UseCases.Product.Commands.Update;
using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;

namespace CleanArch.Application.UseCases.Product.Facade
{
    public interface IProductFacade
    {
        Task<ListProductOutput> ListAsync(ListProductQuery query, CancellationToken cancellationToken);
        Task<GetProductByIdOutput> GetByIdAsync(GetProductByIdQuery query, CancellationToken cancellationToken);
        Task<CreateProductOutput> CreateAsync(CreateProductCommand command, CancellationToken cancellationToken);
        Task Update(UpdateProductCommand command, CancellationToken cancellationToken);
        Task Delete(DeleteProductCommand command, CancellationToken cancellationToken);
    }
}
