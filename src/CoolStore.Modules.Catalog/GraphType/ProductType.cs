using CoolStore.Protobuf.Catalogs.V1;
using HotChocolate.Types;

namespace CoolStore.Modules.Catalog.GraphType
{
    public class ProductType : ObjectType<CatalogProductDto>
    {
        protected override void Configure(IObjectTypeDescriptor<CatalogProductDto> descriptor)
        {
            descriptor.Field(t => t.CalculateSize()).Ignore();
            descriptor.Field(t => t.Clone()).Ignore();
            descriptor.Field(t => t.Equals(null)).Ignore();
        }
    }
}
