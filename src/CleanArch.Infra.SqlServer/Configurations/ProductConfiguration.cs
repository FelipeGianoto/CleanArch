using CleanArch.Domain.Entities;
using CleanArch.Infra.SqlServer.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infra.SqlServer.Configurations
{
    public class ProductConfiguration : BaseEntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(product => product.Name).HasMaxLength(100).IsRequired();
            builder.Property(product => product.Description).HasMaxLength(200).IsRequired();
            builder.Property(product => product.Price).HasPrecision(10, 2);

            builder.HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);
        }
    }
}
