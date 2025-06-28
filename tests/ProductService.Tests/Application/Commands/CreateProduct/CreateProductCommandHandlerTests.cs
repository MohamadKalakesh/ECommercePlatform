using FluentAssertions;
using Moq;
using ProductService.Application.Commands.CreateProduct;
using ProductService.Domain.Entities;
using ProductService.Domain.Interfaces;

namespace ProductService.Tests.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Add_Product_And_Return_Id()
        {
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);
            mockRepo.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(mockRepo.Object);
            var command = new CreateProductCommand
            {
                Name = "Test",
                Description = "Desc",
                Price = 10m
            };

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeEmpty();
            mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p => p.Name == command.Name)), Times.Once);
            mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}
