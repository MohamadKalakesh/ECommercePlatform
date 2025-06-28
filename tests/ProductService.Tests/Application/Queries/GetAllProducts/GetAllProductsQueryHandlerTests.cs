using FluentAssertions;
using Moq;
using ProductService.Application.Queries.GetAllProducts;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Tests.Application.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Return_All_Products()
        {
            var products = new List<Product>
            {
                new Product("P1", "D1", 1m),
                new Product("P2", "D2", 2m)
            };

            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var handler = new GetAllProductsQueryHandler(mockRepo.Object);
            var command = new GetAllProductsQuery();

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.Should().HaveCount(products.Count);
            result.Should().Contain(p => p.Name == "P1");
        }
    }
}
