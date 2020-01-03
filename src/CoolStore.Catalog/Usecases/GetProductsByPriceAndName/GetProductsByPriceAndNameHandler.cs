using CoolStore.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoolStore.Catalog.Usecases.GetProductsByPriceAndName
{
    public class GetProductsByPriceAndNameHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        public GetProductsByPriceAndNameHandler(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IProductRepository ProductRepository { get; }

        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var allProducts = new List<Product>();
            await foreach (var product in ProductRepository.FindAllAsync(new ProductByPriceSpec(request.HighPrice)))
            {
                allProducts.Add(product);
            }

            var limitedProducts = allProducts
                .Skip(request.CurrentPage - 1)
                .Take(10)
                .ToList();

            var response = new GetProductsResponse();
            response.Products.AddRange(limitedProducts.Select(x => new CatalogProductDto
            {
                Id = x.ProductId.Id.ToString(),
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
                InventoryId = x.InventoryId.ToString()
            }));
            return response;
        }
    }
}
