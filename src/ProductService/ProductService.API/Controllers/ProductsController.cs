using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductService.Application.Commands.CreateProduct;
using ProductService.Application.Commands.DeleteProduct;
using ProductService.Application.Commands.UpdateProduct;
using ProductService.Application.DTOs;
using ProductService.Application.Queries.GetAllProducts;
using ProductService.Application.Queries.GetProductById;

namespace ProductService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest("Mismatched ID");

            var succeeded = await _mediator.Send(command);
            return succeeded ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var succeeded = await _mediator.Send(command);
            return succeeded ? NoContent() : NotFound();
        }
    }
}
