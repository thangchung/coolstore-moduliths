using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolStore.Modules.Catalog.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Domain.Product>
    {
        public void Configure(EntityTypeBuilder<Domain.Product> builder)
        {
            builder.ToTable("Products", "catalog");

            builder.HasKey(x => x.ProductId);

            builder
                .Property(x => x.ProductId)
                .HasField("Id")
                .HasColumnName("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(x => x.Id, id => (Domain.ProductId)id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
