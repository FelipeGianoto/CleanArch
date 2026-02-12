using CleanArch.Application.UseCases.Product.Commands.Create;

namespace CleanArch.WebApi.Models.Input.Product
{
    public record CreateProductInput(string Name, string Description, decimal Price, int Stock, string? Image, int CategoryId)
    {
        public CreateProductCommand ToCommand()
            => new(Name, Description, Price, Stock, Image, CategoryId);
    }
}
