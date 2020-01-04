using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Inventory.Domain
{
    public class Inventory : EntityBase<Guid, InventoryId>, IAggregateRoot
    {
        public InventoryId InventoryId => (InventoryId)Id;
        public string Location { get; private set; }
        public string Description { get; private set; }
        public string Website { get; private set; }

        private Inventory() { }

        public static Inventory Of(InventoryId inventoryId, string location, string description, string website)
        {
            return new Inventory
            {
                Id = inventoryId,
                Location = location,
                Description = description,
                Website = website
            };
        }
    }
}
