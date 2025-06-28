using MediatR;

namespace ProductService.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public decimal Price { get; init; }
    }
}
