using CleanArch.Application.UseCases.Category.Commands.Create;
using CleanArch.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace CleanArch.UnitTests.Application.UseCases.Category
{
    public class CreateCategoryHandlerTest
    {
        private readonly Mock<ICategoryRepository> _repositoryMock;
        private readonly CreateCategoryHandler _createCategoryHandler;

        public CreateCategoryHandlerTest()
        {
            _repositoryMock = new Mock<ICategoryRepository>();
            _createCategoryHandler = new CreateCategoryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldCreateCategory_Success()
        {
            // Arrange
            var command = new CreateCategoryCommand("Test Category");
            
            _repositoryMock
                .Setup(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Category>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _createCategoryHandler.HandleAsync(command, CancellationToken.None);
            
            // Assert
            _repositoryMock.Verify(r => r.CreateAsync(It.IsAny<CleanArch.Domain.Entities.Category>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
        }
    }
}
