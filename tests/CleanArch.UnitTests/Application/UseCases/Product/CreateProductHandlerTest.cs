using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Application.UseCases.Product.Commands.Create;
using CleanArch.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace CleanArch.UnitTests.Application.UseCases.Product
{
    public class CreateProductHandlerTest
    {
        private readonly Mock<IProductRepository> _productRepository;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CreateProductHandler _createCategoryHandler;

        public CreateProductHandlerTest()
        {
            _productRepository = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _createCategoryHandler = new CreateProductHandler(_productRepository.Object, _categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldCreateProduct_Success()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Product Description", 10, 20, null, 1);

            _productRepository
                .Setup(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _categoryRepositoryMock
                .Setup(r => r.ExistsByIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _createCategoryHandler.HandleAsync(command, CancellationToken.None);

            // Assert
            _productRepository.Verify(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Product>(), It.IsAny<CancellationToken>()), Times.Once);
            _categoryRepositoryMock.Verify(r => r.ExistsByIdAsync(command.CategoryId, It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task HandleAsync_ShouldCreateProduct_CategoryNotFound()
        {
            // Arrange
            var command = new CreateProductCommand("Test Product", "Product Description", 10, 20, null, 203030);

            _productRepository
                .Setup(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            _categoryRepositoryMock
                .Setup(r => r.ExistsByIdAsync(command.CategoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var act = async () => await _createCategoryHandler.HandleAsync(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Category not found.");

            _productRepository.Verify(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Product>(), It.IsAny<CancellationToken>()), Times.Never);
            _categoryRepositoryMock.Verify(r => r.ExistsByIdAsync(command.CategoryId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
