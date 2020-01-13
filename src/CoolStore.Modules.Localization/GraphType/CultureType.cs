using HotChocolate.Types;

namespace CoolStore.Modules.Localization.GraphType
{
    public class CultureType : ObjectType<Domain.Culture>
    {
        protected override void Configure(IObjectTypeDescriptor<Domain.Culture> descriptor)
        {
        }
    }
}
