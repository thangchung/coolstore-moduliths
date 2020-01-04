using CoolStore.Protobuf.Inventories.V1;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.Modules.Catalog.GraphType
{
    [ExtendObjectType(Name = "Query")]
    public class InventoryQueries
    {
        public async Task<IEnumerable<InventoryDto>> GetInventories([Service] IMediator mediator)
        {
            var response = await mediator.Send(new GetInventoriesRequest { });
            return response.Inventories.ToList();
        }
    }
}
