using CleanArch.Application.UseCases.Category.Commands.Create;

namespace CleanArch.WebApi.Models.Input.Category
{
    public record CreateCategoryInput(string Name)
    {
        public CreateCategoryCommand ToCommand()
            => new(Name);
    }
}
