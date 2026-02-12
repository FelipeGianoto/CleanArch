using CleanArch.Application.UseCases.Category.Commands.Update;

namespace CleanArch.WebApi.Models.Input.Category
{
    public record UpdateCategoryInput(string Name)
    {
        public UpdateCategoryCommand ToCommand(int id)
            => new(id, Name);
    }
}
