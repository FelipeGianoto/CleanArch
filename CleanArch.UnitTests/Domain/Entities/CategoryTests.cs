using CleanArch.Domain.Entities;
using CleanArch.Domain.Validations;
using FluentAssertions;

namespace CleanArch.UnitTests.Domain.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void CreateCategory_WithValidData_ShouldCreateCategory()
        {
            // Arrange
            var name = "Books";
            
            // Act
            var category = new Category(name);

            // Assert
            category.Should().NotBeNull();
            category.Name.Should().Be(name);
        }

        [Fact]
        public void CreateCategory_WithEmptyName_ShouldThrowDomainException()
        {
            // Arrange
            var invalidName = "  ";
            
            // Act & Assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(invalidName));
            exception.Message.Should().Be("Invalid name. Name is required.");
        }

        [Fact]
        public void CreateCategory_WithShortName_ShouldThrowDomainException()
        {
            // Arrange
            var invalidName = "ab";

            // Act & Assert
            var exception = Assert.Throws<DomainExceptionValidation>(() => new Category(invalidName));
            exception.Message.Should().Be("Invalid name, too short, minimum 3 characters.");
        }
    }
}
