using CoolStore.Protobuf.Inventories.V1;
using HotChocolate.Types;

namespace CoolStore.Modules.Catalog.GraphType
{
    public class InventoryType : ObjectType<InventoryDto>
    {
        protected override void Configure(IObjectTypeDescriptor<InventoryDto> descriptor)
        {
            descriptor.Field(t => t.CalculateSize()).Ignore();
            descriptor.Field(t => t.Clone()).Ignore();
            descriptor.Field(t => t.Equals(null)).Ignore();
        }
    }
}
