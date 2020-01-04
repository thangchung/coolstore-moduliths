using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class CatalogProductDto
        : ICatalogProductDto
    {
        public CatalogProductDto(
            string id, 
            string name, 
            string categoryName, 
            string inventoryLocation, 
            double price, 
            string imageUrl, 
            string description)
        {
            Id = id;
            Name = name;
            CategoryName = categoryName;
            InventoryLocation = inventoryLocation;
            Price = price;
            ImageUrl = imageUrl;
            Description = description;
        }

        public string Id { get; }

        public string Name { get; }

        public string CategoryName { get; }

        public string InventoryLocation { get; }

        public double Price { get; }

        public string ImageUrl { get; }

        public string Description { get; }
    }
}
