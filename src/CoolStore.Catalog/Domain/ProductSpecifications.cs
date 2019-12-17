using Moduliths.Domain;

namespace CoolStore.Catalog.Domain
{
    public class ProductWithIdSpecification : SpecificationBase<Product>
    {
        public ProductWithIdSpecification() : base(t => true)
        {
        }
    }
}
