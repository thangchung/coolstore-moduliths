//[MSA]
/*using CoolStore.Protobuf.Catalogs.V1;
using Grpc.Core;
using MediatR;
using System.Threading.Tasks;

namespace CoolStore.Modules.Catalog
{
    public class CatalogService : Protobuf.Catalogs.V1.Catalog.CatalogBase
    {
        private readonly IMediator _mediator;
        public CatalogService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<GetProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override async Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override async Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }

        public override async Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            return await _mediator.Send(request);
        }
    }
}*/
