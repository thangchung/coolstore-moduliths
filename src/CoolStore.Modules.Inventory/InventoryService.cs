//[MSA]
/*using CoolStore.Protobuf.Inventories.V1;
using Grpc.Core;
using MediatR;
using System.Threading.Tasks;

namespace CoolStore.Modules.Inventory
{
    public class InventoryService : Protobuf.Inventories.V1.Inventory.InventoryBase
    {
        private readonly IMediator _mediator;
        public InventoryService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetInventoriesResponse> GetInventories(GetInventoriesRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override async Task<GetInventoryResponse> GetInventory(GetInventoryRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override Task GetInventoryAsyncStream(GetInventoryStreamRequest request, IServerStreamWriter<GetInventoryStreamResponse> responseStream, ServerCallContext context)
        {
            return base.GetInventoryAsyncStream(request, responseStream, context);
        }
    }
}*/