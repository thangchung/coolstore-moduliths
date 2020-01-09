using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolStore.Modules.Catalog.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Category>
    {
        public void Configure(EntityTypeBuilder<Domain.Category> builder)
        {
            builder.ToTable("Categories", "catalog");

            builder.HasKey(x => x.CategoryId);

            builder
                .Property(x => x.CategoryId)
                .HasField("Id")
                .HasColumnName("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(x => x.Id, id => (Domain.CategoryId)id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
