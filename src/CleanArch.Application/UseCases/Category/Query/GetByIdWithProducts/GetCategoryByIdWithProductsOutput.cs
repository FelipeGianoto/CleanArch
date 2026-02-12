namespace CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts
{
    public record GetCategoryByIdWithProductsOutput(int Page, int PageSize, int TotalItens, CategoryByIdWithProductsOutput Category);

    public record CategoryByIdWithProductsOutput(int Id, string Name, IEnumerable<ProductCategoryByIdWithProductsOutput> Products);

    public record ProductCategoryByIdWithProductsOutput(int Id, string Name, string Description, decimal Price, int Stock);
}
