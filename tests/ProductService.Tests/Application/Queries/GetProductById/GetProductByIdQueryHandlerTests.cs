using FluentAssertions;
using Moq;
using ProductService.Application.Queries.GetProductById;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Tests.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_Product_When_Found()
        {
            var product = new Product("Product", "Desc", 12m);
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);

            var handler = new GetProductByIdQueryHandler(mockRepo.Object);
            var query = new GetProductByIdQuery(product.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().NotBeNull();
            result!.Id.Should().Be(product.Id);
            result.Name.Should().Be(product.Name);
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_Not_Found()
        {
            var product = new Product("Product", "Desc", 12m);
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(default(Product));

            var handler = new GetProductByIdQueryHandler(mockRepo.Object);
            var query = new GetProductByIdQuery(product.Id);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Should().BeNull();
        }
    }
}
