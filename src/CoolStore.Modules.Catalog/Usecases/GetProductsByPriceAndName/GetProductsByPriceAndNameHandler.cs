using CoolStore.Modules.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using CoolStore.Protobuf.Inventories.V1;
using MediatR;
using Moduliths.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoolStore.Modules.Catalog.Usecases.GetProductsByPriceAndName
{
    public class GetProductsByPriceAndNameHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        public GetProductsByPriceAndNameHandler(
            IProductRepository productRepository, 
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            ProductRepository = productRepository;
            UnitOfWork = unitOfWork;
            Mediator = mediator;
        }

        public IProductRepository ProductRepository { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IMediator Mediator { get; }

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
            //var inventories = await InventoryClient.GetInventoriesAsync(new GetInventoriesRequest());
            var inventories = await Mediator.Send(new GetInventoriesRequest());

            response.Products.AddRange(limitedProducts.Select(x => {
                var inv = inventories.Inventories.FirstOrDefault();
                return new CatalogProductDto
                {
                    Id = x.ProductId.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    InventoryId = inv != null ? inv.Id : Guid.Empty.ToString(),
                    InventoryLocation = inv != null ? inv.Location : string.Empty,
                    InventoryWebsite = inv != null ? inv.Website : string.Empty,
                    InventoryDescription = inv != null ? inv.Description : string.Empty,
                };
            }));

            await UnitOfWork.CommitAsync();
            return response;
        }
    }
}
