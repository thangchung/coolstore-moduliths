using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolStore.Modules.Localization.Data.Configurations
{
    public class CultureConfiguration : IEntityTypeConfiguration<Domain.Culture>
    {
        public void Configure(EntityTypeBuilder<Domain.Culture> builder)
        {
            builder.ToTable("Cultures", "localization");

            builder.HasKey(x => x.CultureId);

            builder
                .Property(x => x.CultureId)
                .HasField("Id")
                .HasColumnName("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(x => x.Id, id => (Domain.CultureId)id);

            builder.HasMany(x => x.Resources)
                .WithOne(x => x.Culture)
                .IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
