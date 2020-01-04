using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolStore.UI.Blazor.Services
{
    public class CoolStoreService
    {
        private readonly IGraphQLClient _client;

        public CoolStoreService(IGraphQLClient client)
        {
            _client = client;
        }
        
        public async Task<List<ICatalogProductDto>> GetProducts()
        {
            var result = await _client.GetProductsAsync(1, 5000);
            return result.Data.Products.ToList();
        }

        public async Task<List<IInventoryDto>> GetInventories()
        {
            var result = await _client.GetInventoriesAsync();
            return result.Data.Inventories.ToList();
        }
    }
}
