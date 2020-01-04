using CoolStore.Modules.Catalog.GraphType;
using CoolStore.Modules.Inventory.Data;
using CoolStore.Modules.Inventory.Data.Repository;
using CoolStore.Modules.Inventory.Domain;
using FluentValidation;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moduliths.Infra;

namespace CoolStore.Modules.Inventory
{
    public static class Startup
    {
        public static IServiceCollection AddInventoryComponents(this IServiceCollection services, IConfiguration config)
        {
            services.AddServiceByIntefaceInAssembly<Domain.Inventory>(typeof(IValidator<>));
            services.AddDbContext<InventoryDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("MainDb")));
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            return services;
        }

        public static ISchemaBuilder AddInventorySchemaBuilder(this ISchemaBuilder schemaBuilder)
        {
            return schemaBuilder
                .AddType<InventoryQueries>()
                .AddType<InventoryType>();
        }
    }
}