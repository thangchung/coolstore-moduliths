using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using StrawberryShake;
using StrawberryShake.Configuration;
using StrawberryShake.Http;
using StrawberryShake.Http.Subscriptions;
using StrawberryShake.Transport;

namespace CoolStore.UI.Blazor
{
    [System.CodeDom.Compiler.GeneratedCode("StrawberryShake", "11.0.0")]
    public class GetInventoriesResultParser
        : JsonResultParserBase<IGetInventories>
    {
        private readonly IValueSerializer _stringSerializer;

        public GetInventoriesResultParser(IValueSerializerCollection serializerResolver)
        {
            if (serializerResolver is null)
            {
                throw new ArgumentNullException(nameof(serializerResolver));
            }
            _stringSerializer = serializerResolver.Get("String");
        }

        protected override IGetInventories ParserData(JsonElement data)
        {
            return new GetInventories
            (
                ParseGetInventoriesInventories(data, "inventories")
            );

        }

        private IReadOnlyList<IInventoryDto> ParseGetInventoriesInventories(
            JsonElement parent,
            string field)
        {
            if (!parent.TryGetProperty(field, out JsonElement obj))
            {
                return null;
            }

            if (obj.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            int objLength = obj.GetArrayLength();
            var list = new IInventoryDto[objLength];
            for (int objIndex = 0; objIndex < objLength; objIndex++)
            {
                JsonElement element = obj[objIndex];
                list[objIndex] = new InventoryDto
                (
                    DeserializeNullableString(element, "id"),
                    DeserializeNullableString(element, "location"),
                    DeserializeNullableString(element, "website"),
                    DeserializeNullableString(element, "description")
                );

            }

            return list;
        }

        private string DeserializeNullableString(JsonElement obj, string fieldName)
        {
            if (!obj.TryGetProperty(fieldName, out JsonElement value))
            {
                return null;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                return null;
            }

            return (string)_stringSerializer.Deserialize(value.GetString());
        }
    }
}
