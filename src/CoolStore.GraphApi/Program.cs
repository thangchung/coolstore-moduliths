using CoolStore.Modules.Catalog;
using CoolStore.Modules.Catalog.Data;
using CoolStore.Modules.Inventory;
using CoolStore.Modules.Inventory.Data;
using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Playground;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moduliths.Domain;
using Moduliths.Infra.Data;
using Moduliths.Infra.ValidationModel;
using Serilog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoolStore.GraphApi
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            var config = builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false).Build();

            //[MSA]
            /*builder.Host.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel((ctx, options) =>
                {
                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        IdentityModelEventSource.ShowPII = true;
                    }

                    options.Limits.MinRequestBodyDataRate = null;
                    options.Listen(IPAddress.Any, config.GetValue<int>("HttpPort"));
                    options.Listen(IPAddress.Any, config.GetValue<int>("GrpcPort"), listenOptions =>
                    {
                        listenOptions.Protocols = HttpProtocols.Http2;
                    });
                });
            });*/

            builder.Host.UseSerilog();

            builder.Services
                .AddHttpContextAccessor()
                .AddScoped<IUnitOfWork>(s =>
                {
                    var mediator = s.GetService<IMediator>();
                    var catalogDbContext = s.GetService<CatalogDbContext>();
                    var inventoryDbContext = s.GetService<InventoryDbContext>();
                    return new UnitOfWork(new List<DbContext> { catalogDbContext, inventoryDbContext }.ToArray(), mediator);
                })
                .AddCatalogComponents(config)
                .AddInventoryComponents(config)
                .AddMediatR(
                    typeof(Modules.Catalog.Domain.Product).Assembly,
                    typeof(Modules.Inventory.Domain.Inventory).Assembly,
                    typeof(Program).Assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddGraphQL(sp => SchemaBuilder.New()
                    .AddServices(sp)
                    .AddQueryType(d => d.Name("Query"))
                    //.AddMutationType(d => d.Name("Mutation"))
                    .AddCatalogSchemaBuilder()
                    .AddInventorySchemaBuilder()
                    .Create());
                //[MSA]
                //.AddGrpc();

            var app = builder.Build();
            app.Listen($"http://localhost:{config.GetValue<int>("HttpPort")}");

            app.UseStaticFiles();
            app.UseGraphQL("/graphql");
            app.UsePlayground(new PlaygroundOptions
            {
                QueryPath = "/graphql",
                Path = "/playground",
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                //[MSA]
                //endpoints.MapGrpcService<CatalogService>();
                //endpoints.MapGrpcService<InventoryService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World.");
                });
            });
            
            await app.RunAsync();
        }
    }
}
