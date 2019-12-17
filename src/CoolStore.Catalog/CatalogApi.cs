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
using System;
using System.Reflection;
using System.Threading.Tasks;
using static Moduliths.Infra.Helpers.DateTimeHelper;

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
}

namespace CoolStore.Protobuf.Catalogs.V1
{
    public partial class GetProductsRequest : IRequest<GetProductsResponse> { }
    public partial class GetProductByIdRequest : IRequest<GetProductByIdResponse> { }
    public partial class CreateProductRequest : IRequest<CreateProductResponse> { }
    public partial class UpdateProductRequest : IRequest<UpdateProductResponse> { }
    public partial class DeleteProductRequest : IRequest<DeleteProductResponse> { }

    public partial class ProductUpdated : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }

    public partial class ProductDeleted : IDomainEvent
    {
        public DateTime CreatedAt => NewDateTime();
    }
}
