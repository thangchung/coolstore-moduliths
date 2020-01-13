using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Localization.Domain
{
    public class ResourceId : IdentityBase<Guid>
    {
        private ResourceId(Guid id) : base(id) { }
        public static explicit operator ResourceId(Guid id) => id == Guid.Empty ? null : new ResourceId(id);
    }
}
