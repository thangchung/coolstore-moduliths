using Moduliths.Domain;

namespace CoolStore.Modules.Catalog.Domain
{
    public sealed class ProductByPriceSpec : SpecificationBase<Product>
    {
        public ProductByPriceSpec(double price) 
            : base(t => t.Price <= price && !t.IsDeleted)
        {
            AddInclude(t => t.Category);
        }
    }
}
