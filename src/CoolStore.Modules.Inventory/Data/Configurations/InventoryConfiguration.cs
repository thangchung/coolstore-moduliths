using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoolStore.Modules.Inventory.Data.Configurations
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Domain.Inventory>
    {
        public void Configure(EntityTypeBuilder<Domain.Inventory> builder)
        {
            builder.ToTable("Inventories", "inventory");

            builder.HasKey(x => x.InventoryId);

            builder
                .Property(x => x.InventoryId)
                .HasField("Id")
                .HasColumnName("Id")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(x => x.Id, id => (Domain.InventoryId)id);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
