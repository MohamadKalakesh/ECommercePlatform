using FluentAssertions;
using Moq;
using ProductService.Application.Commands.UpdateProduct;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Tests.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Update_Existing_Product()
        {
            var existingProduct = new Product("Old Name", "Old Desc", 1m);
            var mockRepo = new Mock<IProductRepository>();

            mockRepo.Setup(r => r.GetByIdAsync(existingProduct.Id)).ReturnsAsync(existingProduct);
            mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var handler = new UpdateProductCommandHandler(mockRepo.Object);
            var command = new UpdateProductCommand
            {
                Id = existingProduct.Id,
                Name = "New Name",
                Description = "New Desc",
                Price = 9.99m
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
            existingProduct.Name.Should().Be("New Name");
            mockRepo.Verify(r => r.Update(existingProduct), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ReturnFalse_If_Product_NotFound()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(default(Product));

            var handler = new UpdateProductCommandHandler(mockRepo.Object);
            var command = new UpdateProductCommand
            {
                Id = Guid.NewGuid(),
                Name = "X",
                Description = "Y",
                Price = 1
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }
    }
}
