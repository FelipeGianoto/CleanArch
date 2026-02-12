using CleanArch.Domain.Entities.Base;
using CleanArch.Domain.Validations;

namespace CleanArch.Domain.Entities
{
    public sealed class Category : Entity
    {
        public string Name { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; } = [];
        
        public Category(string name)
        {
            ValidateDomain(name);
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public Category(int id, string name, IEnumerable<Product> products)
        {
            ValidateDomain(name);
            Id = id;
            Name = name;
            Products = [.. products];
        }

        public void Update(string name)
        {
            ValidateDomain(name);
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        private static void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name),
                "Invalid name. Name is required.");

            DomainExceptionValidation.When(name.Length < 3,
                "Invalid name, too short, minimum 3 characters.");
        }
    }
}
