using FluentAssertions;
using Moq;
using ProductService.Application.Commands.DeleteProduct;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Tests.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Delete_Product_When_Found()
        {
            var product = new Product("ToDelete", "Desc", 5);
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(product.Id)).ReturnsAsync(product);
            mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var handler = new DeleteProductCommandHandler(mockRepo.Object);
            var command = new DeleteProductCommand(product.Id);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
            mockRepo.Verify(r => r.Delete(product), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_If_Product_Not_Found()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(default(Product));

            var handler = new DeleteProductCommandHandler(mockRepo.Object);
            var command = new DeleteProductCommand(Guid.NewGuid());

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}
