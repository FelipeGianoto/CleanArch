namespace CleanArch.Application.UseCases.Product.Query.GetById
{
    public record GetProductByIdOutput(ProductByIdOutput Product);
    public record ProductByIdOutput(int Id, string Name, string Description, decimal Price, int Stock, string? Image, CategoryProductByIdOutput? Category);
    public record CategoryProductByIdOutput(int Id, string Name);
}
