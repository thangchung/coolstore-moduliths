using CoolStore.Modules.Catalog.Data;
using CoolStore.Modules.Catalog.Data.Repository;
using CoolStore.Modules.Catalog.Domain;
using CoolStore.Modules.Catalog.GraphType;
using FluentValidation;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moduliths.Infra;

namespace CoolStore.Modules.Catalog
{
    public static class Startup
    {
        public static IServiceCollection AddCatalogComponents(this IServiceCollection services, IConfiguration config)
        {
            services.AddServiceByIntefaceInAssembly<Product>(typeof(IValidator<>));
            services.AddDbContext<CatalogDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("MainDb")));
            services.AddScoped<IProductRepository, ProductRepository>();

            //[MSA]
            /*services.AddGrpcClient<Protobuf.Inventories.V1.Inventory.InventoryClient>((s, o) =>
            {
                o.Address = GetCurrentAddress(s);
            });

            static Uri GetCurrentAddress(IServiceProvider serviceProvider)
            {
                // Get the address of the current server from the request
                var context = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("Could not get HttpContext.");
                }

                return new Uri($"{context.Request.Scheme}://{context.Request.Host.Value}");
            }*/

            return services;
        }

        public static ISchemaBuilder AddCatalogSchemaBuilder(this ISchemaBuilder schemaBuilder)
        {
            return schemaBuilder
                .AddType<CatalogQueries>()
                //.AddType<CatalogMutations>()
                .AddType<ProductType>();
        }
    }
}
