namespace CleanArch.Application.UseCases.Product.Commands.Update
{
    public record UpdateProductCommand(int Id, string Name, string Description, decimal Price, int Stock, string? Image, int CategoryId);
}
