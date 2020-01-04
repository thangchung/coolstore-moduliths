using Moduliths.Domain;

namespace CoolStore.Modules.Catalog.Domain
{
    public class ProductByPriceSpec : SpecificationBase<Product>
    {
        public ProductByPriceSpec(double price) 
            : base(t => t.Price <= price && !t.IsDeleted)
        {
        }
    }
}
