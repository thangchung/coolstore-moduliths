using System;
using System.Collections;
using System.Collections.Generic;
using StrawberryShake;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class InventoryDto
        : IInventoryDto
    {
        public InventoryDto(
            string id, 
            string location, 
            string website, 
            string description)
        {
            Id = id;
            Location = location;
            Website = website;
            Description = description;
        }

        public string Id { get; }

        public string Location { get; }

        public string Website { get; }

        public string Description { get; }
    }
}
