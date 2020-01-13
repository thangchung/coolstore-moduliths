using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolStore.Modules.Localization.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Domain.Resource>
    {
        public void Configure(EntityTypeBuilder<Domain.Resource> builder)
        {
            builder.ToTable("Resources", "localization");

            builder.HasKey(x => x.ResourceId);

            builder
                .Property(x => x.ResourceId)
                .HasField("Id")
                .HasColumnName("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(x => x.Id, id => (Domain.ResourceId)id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
