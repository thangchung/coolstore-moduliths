using CoolStore.Protobuf.Catalogs.V1;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.Modules.Catalog.GraphType
{
    [ExtendObjectType(Name = "Query")]
    public class CatalogQueries
    {
        public async Task<IEnumerable<CatalogProductDto>> GetProducts(int currentPage, double highPrice, [Service] IMediator mediator)
        {
            var response = await mediator.Send(new GetProductsRequest { CurrentPage = currentPage, HighPrice = highPrice });
            return response.Products.ToList();
        }
    }
}
