using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoolStore.Modules.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using CoolStore.Protobuf.Inventories.V1;
using MediatR;
using Moduliths.Domain;

namespace CoolStore.Modules.Catalog.Features.GetProductsByPriceAndName
{
    public class GetProductsByPriceAndNameHandler : IRequestHandler<GetProductsRequest, GetProductsResponse>
    {
        public GetProductsByPriceAndNameHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            IMediator mediator)
        {
            ProductRepository = productRepository ?? throw CoreException.NullArgument(nameof(productRepository));
            UnitOfWork = unitOfWork ?? throw CoreException.NullArgument(nameof(unitOfWork));
            Mediator = mediator ?? throw CoreException.NullArgument(nameof(mediator));
        }

        private IProductRepository ProductRepository { get; }
        private IUnitOfWork UnitOfWork { get; }
        private IMediator Mediator { get; }

        public async Task<GetProductsResponse> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var allProducts = new List<Product>();
            await foreach (var product in ProductRepository.FindAllAsync(new ProductByPriceSpec(request.HighPrice)).WithCancellation(cancellationToken))
            {
                allProducts.Add(product);
            }

            var limitedProducts = allProducts
                .Skip(request.CurrentPage - 1)
                .Take(10)
                .ToList();

            var response = new GetProductsResponse();
            var inventories = await Mediator.Send(new GetInventoriesRequest(), cancellationToken);

            response.Products.AddRange(limitedProducts.Select(x =>
            {
                var inv = inventories.Inventories.FirstOrDefault();
                return new CatalogProductDto
                {
                    Id = x.ProductId.Id.ToString(),
                    Name = x.Name,
                    Price = x.Price,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    CategoryId = x.Category.CategoryId.Id.ToString(),
                    CategoryName = x.Category.Name,
                    InventoryId = inv != null ? inv.Id : Guid.Empty.ToString(),
                    InventoryLocation = inv != null ? inv.Location : string.Empty,
                    InventoryWebsite = inv != null ? inv.Website : string.Empty,
                    InventoryDescription = inv != null ? inv.Description : string.Empty,
                };
            }));

            await UnitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }
}
