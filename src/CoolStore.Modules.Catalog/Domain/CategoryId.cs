using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Catalog.Domain
{
    public class CategoryId : IdentityBase<Guid>
    {
        private CategoryId(Guid id) : base(id) { }

        public static explicit operator CategoryId(Guid id) => id == Guid.Empty ? null : new CategoryId(id);
    }
}
