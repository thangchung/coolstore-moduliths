using CoolStore.Protobuf.Inventories.V1;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolStore.Modules.Catalog.Domain
{
    public interface IInventoryGateway
    {
        Task<InventoryDto> GetInventoryAsync(Guid inventoryId);
        Task<IEnumerable<InventoryDto>> GetAvailabilityInventories();
    }
}
