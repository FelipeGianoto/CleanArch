namespace CleanArch.Application.UseCases.Category.Query.GetById
{
    public record GetCategoryByIdOutput(CategoryByIdOutput Category);

    public record CategoryByIdOutput(int Id, string Name, DateTime CreatedAt, DateTime? UpdatedAt);
}
