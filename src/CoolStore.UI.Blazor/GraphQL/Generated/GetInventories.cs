using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetInventories
        : IGetInventories
    {
        public GetInventories(
            IReadOnlyList<IInventoryDto> inventories)
        {
            Inventories = inventories;
        }

        public IReadOnlyList<IInventoryDto> Inventories { get; }
    }
}
