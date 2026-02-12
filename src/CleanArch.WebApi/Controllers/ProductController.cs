using CleanArch.Application.UseCases.Product.Commands.Create;
using CleanArch.Application.UseCases.Product.Commands.Delete;
using CleanArch.Application.UseCases.Product.Facade;
using CleanArch.Application.UseCases.Product.Query.GetById;
using CleanArch.Application.UseCases.Product.Query.List;
using CleanArch.WebApi.Models.Input.Product;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/product")]
    public class ProductController(
        IProductFacade productFacade
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<CreateProductOutput>> Create(
            [FromBody] CreateProductInput input,
            CancellationToken cancellationToken)
        {
            var command = input.ToCommand();
            var output = await productFacade.CreateAsync(command, cancellationToken);

            return CreatedAtAction(
                nameof(GetProductById),
                new { id = output.Id },
                output
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CreateProductOutput>> Update(
            [FromRoute] int id,
            [FromBody] UpdateProductInput input,
            CancellationToken cancellationToken)
        {
            var command = input.ToCommand(id);
            await productFacade.Update(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateProductOutput>> Delete(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var command = new DeleteProductCommand(id);
            await productFacade.Delete(command, cancellationToken);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<ListProductOutput>> List(
            CancellationToken cancellationToken)
        {
            var query = new ListProductQuery();
            var output = await productFacade.ListAsync(query, cancellationToken);
            return Ok(output);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<GetProductByIdOutput>> GetProductById(
            [FromRoute] int id,
            CancellationToken cancellationToken
        )
        {
            var query = new GetProductByIdQuery(id);
            var output = await productFacade.GetByIdAsync(query, cancellationToken);
            return Ok(output);
        }
    }
}
