using CoolStore.Catalog.Data;
using CoolStore.Catalog.Data.Repository;
using CoolStore.Catalog.Domain;
using CoolStore.Protobuf.Catalogs.V1;
using Grpc.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moduliths.Domain;
using System.Reflection;
using System.Threading.Tasks;

namespace CoolStore.Catalog
{
    public static class Startup
    {
        public static IServiceCollection AddCatalogComponents(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(Assembly.GetEntryAssembly(), typeof(Startup).Assembly);

            services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("MainDb")));
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }

    public class CatalogApi : Protobuf.Catalogs.V1.Catalog.CatalogBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CatalogApi> _logger;
        public CatalogApi(IMediator mediator, ILogger<CatalogApi> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public override async Task<GetProductsResponse> GetProducts(GetProductsRequest request, ServerCallContext context)
        {
            /*var result = new GetProductsResponse();

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

            return Task.FromResult(result);*/
            return await _mediator.Send(request);
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
