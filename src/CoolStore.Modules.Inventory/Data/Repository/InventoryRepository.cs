using Moduliths.Infra.Data;

namespace CoolStore.Modules.Inventory.Data.Repository
{
    public class InventoryRepository : RepositoryBase<Domain.Inventory, InventoryDbContext>, Domain.IInventoryRepository
    {
        public InventoryRepository(InventoryDbContext dbContext) : base(dbContext)
        {
        }
    }
}
