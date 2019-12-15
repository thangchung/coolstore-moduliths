using CoolStore.Protobuf.Catalogs.V1;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CoolStore.Catalog
{
    public class CatalogApi : Protobuf.Catalogs.V1.Catalog.CatalogBase
    {
        private readonly ILogger<CatalogApi> _logger;
        public CatalogApi(ILogger<CatalogApi> logger)
        {
            _logger = logger;
        }

        public override Task<GetProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            var result = new GetProductsResponse();

            result.Products.Add(new CatalogProductDto
            {
                Id = Guid.NewGuid().ToString(),
                CategoryId = Guid.NewGuid().ToString(),
                CategoryName = "test",
                Description = "desc",
                ImageUrl = "http://sample.url",
                InventoryDescription = "Inv Desc",
                InventoryId = Guid.NewGuid().ToString(),
                InventoryLocation = "Inv location",
                InventoryWebsite = "inv website",
                Name = "product test",
                Price = 100
            });

            return Task.FromResult(result);
        }

        public override Task<GetProductByIdResponse> GetProductById(GetProductByIdRequest request, ServerCallContext context)
        {
            return base.GetProductById(request, context);
        }

        public override Task<CreateProductResponse> CreateProduct(CreateProductRequest request, ServerCallContext context)
        {
            return base.CreateProduct(request, context);
        }

        public override Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            return base.UpdateProduct(request, context);
        }

        public override Task<DeleteProductResponse> DeleteProduct(DeleteProductRequest request, ServerCallContext context)
        {
            return base.DeleteProduct(request, context);
        }
    }
}
