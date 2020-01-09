using Moduliths.Domain;
using System;

namespace CoolStore.Modules.Catalog.Domain
{
    public class Category : EntityBase<Guid, CategoryId>, IAggregateRoot
    {
        public CategoryId CategoryId => (CategoryId)Id;
        public string Name { get; private set; }

        private Category() { }

        public static Category Of(CategoryId id, string name)
        {
            return new Category { Id = id, Name = name };
        }
    }
}
