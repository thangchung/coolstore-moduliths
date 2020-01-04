using CoolStore.Modules.Inventory.Domain;
using CoolStore.Protobuf.Inventories.V1;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CoolStore.Modules.Inventory.Usecases.GetAvailabilityInventories
{
    public class GetAvailabilityInventoriesHandler : IRequestHandler<GetInventoriesRequest, GetInventoriesResponse>
    {
        public GetAvailabilityInventoriesHandler(IInventoryRepository inventoryRepository)
        {
            InventoryRepository = inventoryRepository;
        }

        public IInventoryRepository InventoryRepository { get; }

        public async Task<GetInventoriesResponse> Handle(GetInventoriesRequest request, CancellationToken cancellationToken)
        {
            var response = new GetInventoriesResponse();
            await foreach (var inventory in InventoryRepository.FindAllAsync())
            {
                response.Inventories.Add(new InventoryDto
                {
                    Id = inventory.InventoryId.Id.ToString(),
                    Location = inventory.Location,
                    Website = inventory.Website,
                    Description = inventory.Description
                });
            }
            return response;
        }
    }
}