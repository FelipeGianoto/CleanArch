using CleanArch.Domain.Entities;
using CleanArch.Domain.Validations;
using FluentAssertions;

namespace CleanArch.UnitTests.Domain.Entities
{
    public class ProductTests
    {
        [Fact]
        public void CreateProduct_WithValidData_ShouldCreateCategory()
        {
            // Arrange
            var name = "Books";
            var description = "A variety of books";
            var price = 19.99m;
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var product = new Product(
                name,
                description,
                price,
                stock,
                image,
                categoryId
            );

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.Description.Should().Be(description);
            product.Price.Should().Be(price);
            product.Stock.Should().Be(stock);
            product.Image.Should().Be(image);
        }

        [Fact]
        public void CreateProduct_WithNullImage_ShouldCreateCategory()
        {
            // Arrange
            var name = "Books";
            var description = "A variety of books";
            var price = 19.99m;
            var stock = 100;
            string? image = null;
            var categoryId = 1;

            // Act
            var product = new Product(
                name,
                description,
                price,
                stock,
                image,
                categoryId
            );

            // Assert
            product.Should().NotBeNull();
            product.Name.Should().Be(name);
            product.Description.Should().Be(description);
            product.Price.Should().Be(price);
            product.Stock.Should().Be(stock);
            product.Image.Should().Be(image);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateProduct_WithNullOrWhiteSpaceName_ShouldThrowDomainException(string invalidName)
        {
            //Arrange
            var description = "A variety of books";
            var price = 19.99m;
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product(invalidName, description, price, stock, image, categoryId));

            // Assert
            exception.Message.Should().Be("Invalid name. Name is required.");
        }

        [Fact]
        public void CreateProduct_WithShortName_ShouldThrowDomainException()
        {
            //Arrange
            var description = "A variety of books";
            var price = 19.99m;
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product("ab", description, price, stock, image, categoryId));

            //Assert
            exception.Message.Should().Be("Invalid name, too short, minimum 3 characters.");
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void CreateProduct_WithNullOrWhiteSpaceDescription_ShouldThrowDomainException(string invalidDescription)
        {
            //Arrange
            var name = "Books";
            var price = 19.99m;
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product(name, invalidDescription!, price, stock, image, categoryId));

            // Assert
            exception.Message.Should().Be("Invalid description. Description is required.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateProduct_WithZeroOrNegativePrice_ShouldThrowDomainException(decimal invalidPrice)
        {
            //Arrange
            var name = "Books";
            var description = "A variety of books";
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product(name, description, invalidPrice, stock, image, categoryId));

            // Assert
            exception.Message.Should().Be("Invalid price value.");
        }

        [Fact]
        public void CreateProduct_WithNegativeStock_ShouldThrowDomainException()
        {
            //Arrange
            var name = "Books";
            var description = "A variety of books";
            var stock = 100;
            var image = "books.jpg";
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product(name, description, stock, -1, image, categoryId));

            // Assert
            exception.Message.Should().Be("Invalid stock value.");
        }

        [Fact]
        public void CreateProduct_WithImageNameTooLong_ShouldThrowDomainException()
        {
            //Arrange
            var name = "Books";
            var description = "A variety of books";
            var stock = 100;
            var price = 19.99m;
            var longImageName = new string('a', 251);
            var categoryId = 1;

            // Act
            var exception = Assert.Throws<DomainExceptionValidation>(() =>
                new Product(name, description, price, stock, longImageName, categoryId));

            // Assert
            exception.Message.Should().Be("Invalid image name, too long, maximum 250 characters.");
        }
    }
}
