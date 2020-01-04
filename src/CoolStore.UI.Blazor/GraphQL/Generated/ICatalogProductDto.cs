using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public interface ICatalogProductDto
    {
        string Id { get; }

        string Name { get; }

        string CategoryName { get; }

        string InventoryLocation { get; }

        double Price { get; }

        string ImageUrl { get; }

        string Description { get; }
    }
}
