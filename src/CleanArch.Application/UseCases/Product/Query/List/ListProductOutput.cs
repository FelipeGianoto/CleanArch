namespace CleanArch.Application.UseCases.Product.Query.List
{
    public record ListProductOutput(IEnumerable<ProductListOutput> Products);
    public record ProductListOutput(int Id, string Name, string Description, decimal Price, int Stock, string? Image, int CategoryId);
}
