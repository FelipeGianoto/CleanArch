using CleanArch.Domain.Entities.Base;
using CleanArch.Domain.Validations;

namespace CleanArch.Domain.Entities
{
    public sealed class Product : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public string? Image { get; private set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public Product(string name, string description, decimal price, int stock, string? image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string description, decimal price, int stock, string? image, int categoryId)
        {
            ValidateDomain(name, description, price, stock, image);
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
            Image = image;
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        private static void ValidateDomain(
            string name,
            string description,
            decimal price,
            int stock,
            string? image)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");

            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(description),
                "Invalid description. Description is required.");

            DomainExceptionValidation.When(price <= 0,
                "Invalid price value.");

            DomainExceptionValidation.When(stock < 0,
                "Invalid stock value.");

            DomainExceptionValidation.When(image?.Length > 250,
                "Invalid image name, too long, maximum 250 characters.");
        }
    }
}
