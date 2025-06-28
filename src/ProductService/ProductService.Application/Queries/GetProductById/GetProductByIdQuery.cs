using MediatR;
using ProductService.Application.DTOs;

namespace ProductService.Application.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
