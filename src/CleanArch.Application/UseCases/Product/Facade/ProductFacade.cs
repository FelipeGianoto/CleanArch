using CleanArch.Application.Abstractions.Commands;
using CleanArch.Application.Abstractions.Queries;
using CleanArch.Application.UseCases.Product.Commands.Create;
using CleanArch.Application.UseCases.Product.Commands.Delete;
using CleanArch.Application.UseCases.Product.Commands.Update;
using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;

namespace CleanArch.Application.UseCases.Product.Facade
{
    public class ProductFacade(
         IQueryHandler<ListProductQuery, ListProductOutput> listHandler,
        IQueryHandler<GetProductByIdQuery, GetProductByIdOutput> getByIdHandler,
        ICommandHandler<CreateProductCommand, CreateProductOutput> createHandler,
        ICommandHandler<UpdateProductCommand> updateHanler,
        ICommandHandler<DeleteProductCommand> deleteHanler
    ) : IProductFacade
    {
        public Task<ListProductOutput> ListAsync(ListProductQuery query, CancellationToken cancellationToken)
            => listHandler.HandleAsync(query, cancellationToken);

        public Task<GetProductByIdOutput> GetByIdAsync(GetProductByIdQuery query, CancellationToken cancellationToken)
            => getByIdHandler.HandleAsync(query, cancellationToken);

        public Task<CreateProductOutput> CreateAsync(CreateProductCommand command, CancellationToken cancellationToken)
            => createHandler.HandleAsync(command, cancellationToken);

        public Task Update(UpdateProductCommand command, CancellationToken cancellationToken)
            => updateHanler.HandleAsync(command, cancellationToken);

        public Task Delete(DeleteProductCommand command, CancellationToken cancellationToken)
            => deleteHanler.HandleAsync(command, cancellationToken);
    }
}
