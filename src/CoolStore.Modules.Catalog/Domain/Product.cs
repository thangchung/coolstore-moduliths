using CoolStore.Protobuf.Catalogs.V1;
using Moduliths.Domain;
using Moduliths.Infra.Extensions;
using System;
using static Moduliths.Infra.Helpers.DateTimeHelper;

namespace CoolStore.Modules.Catalog.Domain
{
    public class Product : EntityBase<Guid, ProductId>, IAggregateRoot
    {
        public ProductId ProductId => (ProductId)Id;
        public string Name { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public string ImageUrl { get; private set; }
        public Guid InventoryId { get; private set; }
        public bool IsDeleted { get; private set; }
        public CategoryId CategoryId { get; private set; }
        public Category Category { get; private set; }

        private Product() { }

        public static Product Of(ProductId productId, CreateProductRequest request)
        {
            return new Product
            {
                Id = productId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl,
                InventoryId = string.IsNullOrEmpty(request.InventoryId)
                    ? Guid.Empty : request.InventoryId.ConvertTo<Guid>(),
                CategoryId = (CategoryId)request.CategoryId.ConvertTo<Guid>(),
                Updated = NewDateTime(),
                IsDeleted = false
            };
        }

        public Product UpdateProduct(UpdateProductRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            Price = request.Price;
            ImageUrl = request.ImageUrl;

            if (!string.IsNullOrEmpty(request.InventoryId))
            {
                InventoryId = request.InventoryId.ConvertTo<Guid>();
            }

            Updated = NewDateTime();

            AddDomainEvent(new ProductUpdated
            {
                ProductId = Id.ToString(),
                Name = Name,
                Price = Price,
                ImageUrl = ImageUrl,
                Description = Description
            });

            return this;
        }

        public Product MarkAsDeleted()
        {
            IsDeleted = true;

            AddDomainEvent(new ProductDeleted
            {
                ProductId = Id.ToString()
            });

            return this;
        }
    }
}
