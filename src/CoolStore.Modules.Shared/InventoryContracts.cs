using MediatR;

namespace CoolStore.Protobuf.Inventories.V1
{
    public partial class GetInventoriesRequest : IRequest<GetInventoriesResponse> { }
    public partial class GetInventoryStreamRequest : IRequest<GetInventoryStreamResponse> { }
    public partial class GetInventoryRequest : IRequest<GetInventoryResponse> { }
}
