using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Application.UseCases.Category.Commands.Delete;
using CleanArch.Application.UseCases.Category.Facade;
using CleanArch.Application.UseCases.Category.Query.GetById;
using CleanArch.Application.UseCases.Category.Query.GetByIdWithProducts;
using CleanArch.Application.UseCases.Category.Query.List;
using CleanArch.WebApi.Models.Input.Category;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    public class CategoryController(
        ICategoryFacade categoryFacade
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateCategoryOutput>> Create(
            [FromBody] CreateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var command = input.ToCommand();
            var output = await categoryFacade.CreateAsync(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = output.Id },
                output
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CreateCategoryOutput>> Update(
            [FromRoute] int id,
            [FromBody] UpdateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var command = input.ToCommand(id);
            await categoryFacade.Update(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateCategoryOutput>> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id);
            await categoryFacade.Delete(command, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ListCategoryOutput>> List(
            CancellationToken cancellationToken)
        {
            var query = new ListCategoryQuery();
            var output = await categoryFacade.ListAsync(query, cancellationToken);
            return Ok(output);
        }

        [HttpGet("{id}/with-products")]
        public async Task<ActionResult<GetCategoryByIdWithProductsOutput>> GetWithProducts(
            [FromRoute] int id,
            [FromQuery] GetCategoryByIdWithProductsinput input,
            CancellationToken cancellationToken)
        {
            var query = input.ToQuery(id);
            var output = await categoryFacade.GetByIdWithProductsAsync(query, cancellationToken);
            return Ok(output);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<ActionResult<GetCategoryByIdOutput>> GetCategoryById(
            [FromRoute] int id,
            CancellationToken cancellationToken
        )
        {
            var query = new GetCategoryByIdQuery(id);
            var output = await categoryFacade.GetByIdAsync(query, cancellationToken);
            return Ok(output);
        }
    }
}
