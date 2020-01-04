using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Inventory.Domain
{
    public class InventoryId : IdentityBase<Guid>
    {
        private InventoryId(Guid id) : base(id) { }

        public static explicit operator InventoryId(Guid id) => id == Guid.Empty ? null : new InventoryId(id);
    }
}
