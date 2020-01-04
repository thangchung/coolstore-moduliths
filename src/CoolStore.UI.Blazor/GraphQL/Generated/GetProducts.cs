using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetProducts
        : IGetProducts
    {
        public GetProducts(
            IReadOnlyList<ICatalogProductDto> products)
        {
            Products = products;
        }

        public IReadOnlyList<ICatalogProductDto> Products { get; }
    }
}
