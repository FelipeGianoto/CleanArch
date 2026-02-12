using CleanArch.Domain.Entities;
using CleanArch.Infra.SqlServer.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArch.Infra.SqlServer.Configurations
{
    public class CategoryConfiguration : BaseEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);
            builder.Property(category => category.Name).HasMaxLength(100).IsRequired();

            builder.HasIndex(category => category.Name).IsUnique();
        }
    }
}
