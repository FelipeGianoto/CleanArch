namespace CleanArch.Application.UseCases.Category.Query.List
{
    public record ListCategoryOutput(IEnumerable<CategoryListOutput> Categories);

    public record CategoryListOutput(int Id, string Name);
}
