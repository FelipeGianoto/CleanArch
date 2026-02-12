namespace CleanArch.Application.Abstractions.Commands
{
    public interface ICommandHandler<TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
    }

    public interface ICommandHandler<TQuery>
    {
        Task HandleAsync(TQuery query, CancellationToken cancellationToken);
    }
}
