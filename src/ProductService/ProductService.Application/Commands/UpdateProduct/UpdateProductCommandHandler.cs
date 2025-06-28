using MediatR;
using ProductService.Domain.Interfaces;

namespace ProductService.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null)
                return false;

            product.UpdateDetails(request.Name, request.Description, request.Price);
            _repository.Update(product);
            return await _repository.SaveChangesAsync();
        }
    }
}
