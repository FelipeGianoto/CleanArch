using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;

namespace CleanArch.WebApi.Models.Input.Category
{
    public record GetCategoryByIdWithProductsinput(int Page = 1, int PageSize = 10)
    {
        public GetCategoryByIdWithProductsQuery ToQuery(int Id)
            => new(Id, Page, PageSize);
    }
}
