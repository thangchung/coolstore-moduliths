using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Catalog.Domain
{
    public class ProductId : IdentityBase<Guid>
    {
        private ProductId(Guid id) : base(id) { }

        public static explicit operator ProductId(Guid id) => id == Guid.Empty ? null : new ProductId(id);
    }
}
