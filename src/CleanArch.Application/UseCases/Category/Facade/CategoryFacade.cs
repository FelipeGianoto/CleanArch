using CleanArch.Application.Abstractions.Commands;
using CleanArch.Application.Abstractions.Queries;
using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Application.UseCases.Category.Commands.Delete;
using CleanArch.Application.UseCases.Category.Commands.Update;
using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;

namespace CleanArch.Application.UseCases.Category.Facade
{
    public sealed class CategoryFacade(
        IQueryHandler<ListCategoryQuery, ListCategoryOutput> listHandler,
        IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdOutput> getByIdHandler,
        IQueryHandler<GetCategoryByIdWithProductsQuery, GetCategoryByIdWithProductsOutput> getByIdWithProductsHandler,
        ICommandHandler<CreateCategoryCommand, CreateCategoryOutput> createHandler,
        ICommandHandler<UpdateCategoryCommand> updateHanler,
        ICommandHandler<DeleteCategoryCommand> deleteHanler
    ) : ICategoryFacade
    {
        public Task<ListCategoryOutput> ListAsync(ListCategoryQuery query, CancellationToken cancellationToken)
            => listHandler.HandleAsync(query, cancellationToken);

        public Task<GetCategoryByIdOutput> GetByIdAsync(GetCategoryByIdQuery query, CancellationToken cancellationToken)
            => getByIdHandler.HandleAsync(query, cancellationToken);

        public Task<GetCategoryByIdWithProductsOutput> GetByIdWithProductsAsync(GetCategoryByIdWithProductsQuery query, CancellationToken cancellationToken)
            => getByIdWithProductsHandler.HandleAsync(query, cancellationToken);

        public Task<CreateCategoryOutput> CreateAsync(CreateCategoryCommand command, CancellationToken cancellationToken)
            => createHandler.HandleAsync(command, cancellationToken);

        public Task Update(UpdateCategoryCommand command, CancellationToken cancellationToken)
            => updateHanler.HandleAsync(command, cancellationToken);

        public Task Delete(DeleteCategoryCommand command, CancellationToken cancellationToken)
            => deleteHanler.HandleAsync(command, cancellationToken);
    }
}
