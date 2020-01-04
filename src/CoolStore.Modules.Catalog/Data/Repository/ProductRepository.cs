using CoolStore.Modules.Catalog.Domain;
using Moduliths.Infra.Data;

namespace CoolStore.Modules.Catalog.Data.Repository
{
    public class ProductRepository : RepositoryBase<Product, CatalogDbContext>, IProductRepository
    {
        public ProductRepository(CatalogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
