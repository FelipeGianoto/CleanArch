namespace CleanArch.Application.UseCases.Product.Commands.Create
{
    public record CreateProductCommand(string Name, string Description, decimal Price, int Stock, string? Image, int CategoryId);
}
