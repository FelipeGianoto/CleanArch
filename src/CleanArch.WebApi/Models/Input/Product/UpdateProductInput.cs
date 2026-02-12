using CleanArch.Application.UseCases.Product.Commands.Update;

namespace CleanArch.WebApi.Models.Input.Product
{
    public record UpdateProductInput(string Name, string Description, decimal Price, int Stock, string? Image, int CategoryId)
    {
        public UpdateProductCommand ToCommand(int id)
            => new (id, Name, Description, Price, Stock, Image, CategoryId);
    }
}
