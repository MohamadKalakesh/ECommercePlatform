using MediatR;

namespace ProductService.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public decimal Price { get; init; }
    }
}
