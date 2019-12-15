using CoolStore.Protobuf.Catalogs.V1;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.UI.Services
{
    public class CoolStoreService
    {
        public CoolStoreService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<List<CatalogProductDto>> GetProducts()
        {
            var grpcUri = Configuration["CoolStoreApi:GrpcBaseAddress"];
            using var channel = GrpcChannel.ForAddress(grpcUri);
            var client = new Catalog.CatalogClient(channel);
            var result = await client.GetProductsAsync(new GetProductsRequest { CurrentPage = 1, HighPrice = 100 }, null, DateTime.UtcNow + TimeSpan.FromSeconds(10));
            return result.Products.ToList();
        }
    }
}
